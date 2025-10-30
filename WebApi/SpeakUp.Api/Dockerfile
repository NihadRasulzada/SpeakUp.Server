FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi/SpeakUp.Api/SpeakUp.Api.csproj", "WebApi/SpeakUp.Api/"]
COPY ["Core/SpeakUp.Application/SpeakUp.Application.csproj", "Core/SpeakUp.Application/"]
COPY ["Core/SpeakUp.Domain/SpeakUp.Domain.csproj", "Core/SpeakUp.Domain/"]
COPY ["Infrastructure/SpeakUp.Persistence/SpeakUp.Persistence.csproj", "Infrastructure/SpeakUp.Persistence/"]
RUN dotnet restore "WebApi/SpeakUp.Api/SpeakUp.Api.csproj"
COPY . .
WORKDIR "/src/WebApi/SpeakUp.Api"
RUN dotnet build "./SpeakUp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpeakUp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpeakUp.Api.dll"]
