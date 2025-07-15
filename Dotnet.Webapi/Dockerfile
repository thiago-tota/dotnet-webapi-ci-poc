# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

# Install ICU libraries for globalization support in Alpine (as root)
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Switch to non-root user and set working directory
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file to their respective directories
COPY ["Dotnet.Webapi/Dotnet.Webapi.csproj", "Dotnet.Webapi/"]

# Restore the project dependencies
RUN dotnet restore "./Dotnet.Webapi/Dotnet.Webapi.csproj"

# Copy all the source code to the container
COPY . .
# Set the working directory to the service project
WORKDIR "/src/Dotnet.Webapi"

# Build the service project without restoring dependencies again
RUN dotnet build "./Dotnet.Webapi.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Dotnet.Webapi.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Optional: Healthcheck for the container (requires curl to be installed)
# RUN apk add --no-cache curl
# HEALTHCHECK --interval=30s --timeout=10s --start-period=5s CMD curl --fail http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "Dotnet.Webapi.dll"]