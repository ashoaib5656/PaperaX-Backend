using Microsoft.EntityFrameworkCore;
using PaperaX.Infrastructure.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PaperaX.Infrastructure.Email;
using PaperaX.Application.Features.Auth.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add User Secrets in Development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Validate required configuration on startup
var requiredSettings = new[]
{
    "ConnectionStrings:DefaultConnection",
    "Redis:ConnectionString",
    "JwtSettings:Secret",
    "GoogleAuthSettings:ClientId",
    "GoogleAuthSettings:ClientSecret",
    "EmailSettings:FromEmail",
    "EmailSettings:SmtpServer",
    "EmailSettings:SmtpUsername",
    "EmailSettings:SmtpPassword"
};

foreach (var setting in requiredSettings)
{
    if (string.IsNullOrEmpty(builder.Configuration[setting]))
    {
        throw new InvalidOperationException(
            $"Missing required configuration: {setting}. Set it via environment variables, User Secrets, or appsettings.");
    }
}

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173") // Common React/Vite ports
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// DbContext

builder.Services.AddDbContext<PaperaX.Infrastructure.Persistence.ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Redis Connection
builder.Services.AddSingleton(sp =>
{
    var connectinString = builder.Configuration["Redis:ConnectionString"] ?? "localhost:6379";
    return new RedisConnection(connectinString);
});

// JWT

var jwtSetting = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSetting["Issuer"],
        ValidAudience = jwtSetting["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["Secret"]!)),
        ClockSkew = TimeSpan.Zero
    };

});

// SMTP
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Redis OTP Service
builder.Services.AddScoped<OtpRedisService>();

// Dependency Injection for Auth Services
builder.Services.AddScoped<PaperaX.Application.Features.Auth.Interfaces.IGoogleAuthService, PaperaX.Infrastructure.Authentication.GoogleAuthService>();
builder.Services.AddScoped<PaperaX.Application.Features.Auth.Interfaces.IJwtTokenGenerator, PaperaX.Infrastructure.Authentication.JwtTokenGenerator>();
builder.Services.AddScoped<PaperaX.Application.Features.Auth.Interfaces.IAuthService, PaperaX.Infrastructure.Authentication.AuthService>();

builder.Services.AddScoped<IEmailService, EmailService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
