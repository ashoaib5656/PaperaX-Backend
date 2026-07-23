using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Infrastructure.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PaperaX.Infrastructure.Email;
using PaperaX.Application.Features.Auth.Interfaces;
using PaperaX.Infrastructure.Authentication;
using Serilog;
using PaperaX.Api.Middleware;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;
using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
{
    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

    IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
    {
        { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
        { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
        { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
        { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
        { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
        { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) }
    };

    loggerConfig.ReadFrom.Configuration(context.Configuration)
                .WriteTo.PostgreSQL(
                    connectionString: connectionString,
                    tableName: "ErrorLogs",
                    columnOptions: columnWriters,
                    needAutoCreateTable: true,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
                );
});

// Add User Secrets in Development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// ---------------------------
// Configuration / Options Binding
// ---------------------------
builder.Services.AddOptions<JwtSettings>()
    .BindConfiguration("JwtSettings")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<GoogleAuthSettings>()
    .BindConfiguration("GoogleAuthSettings")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<RedisSettings>()
    .BindConfiguration("Redis")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<EmailSettings>()
    .BindConfiguration("EmailSettings")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<Resend.ResendClientOptions>()
    .Configure<IOptions<EmailSettings>>((options, emailSettings) => 
    {
        options.ApiToken = emailSettings.Value.ResendApiKey;
    });
builder.Services.AddHttpClient<Resend.IResend, Resend.ResendClient>();

// ---------------------------
// Add services to the container.
// ---------------------------

// Global Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        // CORS is defense-in-depth: it blocks direct browser requests to the Render API.
        // In production the browser only calls the Vercel origin (proxy forwards server-side),
        // so CORS is not what makes cookies work — the proxy is.
        var allowedOrigins = builder.Environment.IsDevelopment()
            ? builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                ?? new[] { "http://localhost:3000", "http://localhost:5173" }
            : builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                ?? new[] { "https://paperax.vercel.app" };

        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// DbContext (with Resilience)
builder.Services.AddDbContext<PaperaX.Infrastructure.Persistence.ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(3)));

builder.Services.AddScoped<PaperaX.Application.Interfaces.IApplicationDbContext>(provider => provider.GetRequiredService<PaperaX.Infrastructure.Persistence.ApplicationDbContext>());

// Hangfire
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddHangfireServer();

// Add Background Job Services
builder.Services.AddTransient<PaperaX.Infrastructure.BackgroundJobs.BannerStatusJob>();

// Redis Connection
builder.Services.AddSingleton(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    return new RedisConnection(redisSettings.ConnectionString);
});

builder.Services.AddSingleton<PaperaX.Application.Interfaces.ICacheService, PaperaX.Infrastructure.Caching.RedisCacheService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
    .Configure<IOptions<JwtSettings>>((options, jwtSettings) =>
    {
        var settings = jwtSettings.Value;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.Issuer,
            ValidAudience = settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret)),
            ClockSkew = TimeSpan.Zero
        };
    });


// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedPolicy", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);
        opt.PermitLimit = 30;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
});

// Health Checks
builder.Services.AddHealthChecks();

// Redis OTP Service
builder.Services.AddScoped<OtpRedisService>();

// Dependency Injection Services
builder.Services.AddScoped<PaperaX.Application.Features.Auth.Interfaces.IGoogleAuthService, PaperaX.Infrastructure.Authentication.GoogleAuthService>();
builder.Services.AddScoped<PaperaX.Application.Features.Auth.Interfaces.IJwtTokenGenerator, PaperaX.Infrastructure.Authentication.JwtTokenGenerator>();
builder.Services.AddScoped<PaperaX.Application.Features.Auth.Interfaces.IAuthService, PaperaX.Infrastructure.Authentication.AuthService>();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(PaperaX.Application.Features.Categories.Commands.CreateCategory.CreateCategoryCommand).Assembly);
    cfg.AddBehavior(typeof(MediatR.Pipeline.IRequestPreProcessor<>), typeof(PaperaX.Application.Common.Behaviors.LoggingBehavior<>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PaperaX.Application.Common.Behaviors.UnhandledExceptionBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PaperaX.Application.Common.Behaviors.ValidationBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PaperaX.Application.Common.Behaviors.PerformanceBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PaperaX.Application.Common.Behaviors.TransactionBehavior<,>));
});

builder.Services.AddScoped<IEmailService, EmailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PaperaX.Infrastructure.Persistence.ApplicationDbContext>();
    context.Database.Migrate();
}

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.UseHangfireDashboard();

// Register Recurring Jobs
using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobManager.AddOrUpdate<PaperaX.Infrastructure.BackgroundJobs.BannerStatusJob>(
        "banner-status-job",
        job => job.ExecuteAsync(),
        Cron.Minutely);
}

app.Run();
