FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

# Make sure the /https folder exists inside the container
RUN mkdir /https

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy the full source code (required to resolve project references)
COPY . .
# Copy cert into container
COPY aspnetapp.pfx /https/aspnetapp.pfx

# Restore using the main API project
RUN dotnet restore src/API/Evently.Api/Evently.Api.csproj

# Build
WORKDIR /src/src/API/Evently.Api
RUN dotnet build Evently.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Evently.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evently.Api.dll"]
