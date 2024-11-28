# Base image with ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
ARG APP_UID=1000
USER $APP_UID
WORKDIR /app
EXPOSE 5000

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Build image with SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy and restore project dependencies
COPY AuxiliarContabil.API/*.csproj AuxiliarContabil.API/
COPY AuxiliarContabil.Application/*.csproj AuxiliarContabil.Application/
COPY AuxiliarContabil.Domain/*.csproj AuxiliarContabil.Domain/
COPY AuxiliarContabil.Infrastructure/*.csproj AuxiliarContabil.Infrastructure/
RUN dotnet restore "AuxiliarContabil.API/AuxiliarContabil.API.csproj"

# Copy source code and build
COPY . .
WORKDIR "/src/AuxiliarContabil.API"
RUN dotnet build "AuxiliarContabil.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuxiliarContabil.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuxiliarContabil.API.dll"]
