# The stage that serves as the base of the image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

# The stage that compile or build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG BUILD_CONFIG=Release
WORKDIR /src
COPY ["GovConnect.Api/GovConnect.Api.csproj", "GovConnect.Api/"]
RUN dotnet restore "./GovConnect.Api/GovConnect.Api.csproj"
COPY . . 
WORKDIR "/src/GovConnect.Api"
RUN dotnet build "./GovConnect.Api.csproj" -c $BUILD_CONFIG -o /app/build

# The stage where the project will be published
FROM build-env AS publish
ARG BUILD_CONFIG=Release
RUN dotnet publish "./GovConnect.Api.csproj" -c $BUILD_CONFIG -o /app/publish /p:UseAppHost=false

# The final stage of everything
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GovConnect.Api.dll"]
