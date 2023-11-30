FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MainTz.AuthApi/MainTz.AuthApi.csproj", "MainTz.AuthApi/"]
COPY ["Extensions/Extensions.csproj", "Extensions/"]
COPY ["MainTz.RestApi/MainTz.RestApi.csproj", "MainTz.RestApi/"]
RUN dotnet restore "./MainTz.AuthApi/./MainTz.AuthApi.csproj"
COPY . .
WORKDIR "/src/MainTz.AuthApi"
RUN dotnet build "./MainTz.AuthApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MainTz.AuthApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MainTz.AuthApi.dll"]