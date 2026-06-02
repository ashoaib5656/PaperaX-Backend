FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first to cache restore layers
COPY ["PaperaX.Api.sln", "./"]
COPY ["src/PaperaX.Api/PaperaX.Api.csproj", "src/PaperaX.Api/"]
COPY ["src/PaperaX.Application/PaperaX.Application.csproj", "src/PaperaX.Application/"]
COPY ["src/PaperaX.Domain/PaperaX.Domain.csproj", "src/PaperaX.Domain/"]
COPY ["src/PaperaX.Infrastructure/PaperaX.Infrastructure.csproj", "src/PaperaX.Infrastructure/"]
COPY ["src/PaperaX.Shared/PaperaX.Shared.csproj", "src/PaperaX.Shared/"]

# Restore dependencies
RUN dotnet restore "src/PaperaX.Api/PaperaX.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build and publish
WORKDIR "/src/src/PaperaX.Api"
RUN dotnet publish "PaperaX.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Generate the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "PaperaX.Api.dll"]
