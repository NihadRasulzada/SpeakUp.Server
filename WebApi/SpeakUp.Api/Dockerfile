# Base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# COPY csproj-lar konteksə uyğun
COPY ["Common/SpeakUp.Common/SpeakUp.Common/SpeakUp.Common.csproj", "Common/SpeakUp.Common/"]
COPY ["API/SpeakUp.Server/Core/SpeakUp.Application/SpeakUp.Application.csproj", "Core/SpeakUp.Application/"]
COPY ["API/SpeakUp.Server/Core/SpeakUp.Domain/SpeakUp.Domain.csproj", "Core/SpeakUp.Domain/"]
COPY ["API/SpeakUp.Server/Infrastructure/SpeakUp.Persistence/SpeakUp.Persistence.csproj", "Infrastructure/SpeakUp.Persistence/"]
COPY ["API/SpeakUp.Server/WebApi/SpeakUp.Api/SpeakUp.Api.csproj", "WebApi/SpeakUp.Api/"]

# Restore dependencies
RUN dotnet restore "WebApi/SpeakUp.Api/SpeakUp.Api.csproj"

# COPY bütün kod
COPY . .

# Build
WORKDIR "/src/WebApi/SpeakUp.Api"
RUN dotnet build "./SpeakUp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpeakUp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpeakUp.Api.dll"]