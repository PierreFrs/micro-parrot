FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["ai-service.sln", "./"]
COPY ["src/MicroParrot.AI.Api/MicroParrot.AI.Api.csproj", "src/MicroParrot.AI.Api/"]
COPY ["src/MicroParrot.AI.Core/MicroParrot.AI.Core.csproj", "src/MicroParrot.AI.Core/"]
COPY ["src/MicroParrot.AI.Infrastructure/MicroParrot.AI.Infrastructure.csproj", "src/MicroParrot.AI.Infrastructure/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Build and publish
RUN dotnet publish "src/MicroParrot.AI.Api/MicroParrot.AI.Api.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MicroParrot.AI.Api.dll"] 