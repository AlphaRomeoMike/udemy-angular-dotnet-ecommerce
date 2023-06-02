# Base image with SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

# Copy and restore NuGet packages
COPY ./API/*.csproj ./API/
COPY ./Infrastructure/*.csproj ./Infrastructure/
COPY ./Core/*.csproj ./Core/

RUN dotnet restore ./API/API.csproj

# Copy the source code
COPY . .

# Build the application
WORKDIR /app/API

RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Final image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

# Expose the necessary port
EXPOSE 5001

# Set the entry point
ENTRYPOINT ["dotnet", "API.dll"]
