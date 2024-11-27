FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuxiliarContabil.API/AuxiliarContabil.API.csproj", "AuxiliarContabil.API/"]
COPY ["AuxiliarContabil.Application/AuxiliarContabil.Application.csproj", "AuxiliarContabil.Application/"]
COPY ["AuxiliarContabil.Domain/AuxiliarContabil.Domain.csproj", "AuxiliarContabil.Domain/"]
COPY ["AuxiliarContabil.Infrastructure/AuxiliarContabil.Infrastructure.csproj", "AuxiliarContabil.Infrastructure/"]
RUN dotnet restore "AuxiliarContabil.API/AuxiliarContabil.API.csproj"
COPY . .
WORKDIR "/src/AuxiliarContabil.API"
RUN dotnet build "AuxiliarContabil.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuxiliarContabil.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuxiliarContabil.API.dll"]
