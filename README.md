# .NET Web API CI/CD POC

A Proof of Concept (POC) demonstrating CI/CD pipeline for a .NET 8 Web API using GitHub Actions and JFrog Artifactory.

## Features

- **🚀 .NET 8 Web API**: Modern ASP.NET Core Web API with Weather Forecast sample
- **🐳 Alpine Docker**: Lightweight Alpine Linux-based Docker containers (181MB)
- **⚡ CI/CD Pipeline**: Automated GitHub Actions workflow
- **📦 JFrog Integration**: Docker image publishing to JFrog Artifactory
- **🧪 Automated Testing**: Unit tests with test result reporting
- **🔒 Security**: Non-root Docker containers and proper secrets management

## Project Structure

```
dotnet-webapi/
├── Dotnet.Webapi/                 # Main Web API project
│   ├── Controllers/               # API controllers
│   ├── Dockerfile                 # Alpine-based container definition
│   └── Program.cs                 # Application entry point
├── Dotnet.Webapi.Tests/          # Unit test project
├── .github/workflows/             # GitHub Actions workflows
│   └── ci-pipeline.yml           # Main CI/CD pipeline
└── dotnet-webapi.sln             # Visual Studio solution
```

## Local Development

### Prerequisites

- .NET 8 SDK
- Docker Desktop

### Running Locally

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build --configuration Release

# Run tests
dotnet test

# Run the API
dotnet run --project Dotnet.Webapi
```

The API will be available at `https://localhost:7042` or `http://localhost:5042`.

### Docker Build

```bash
# Build the Docker image
docker build -f Dotnet.Webapi/Dockerfile -t dotnet-webapi .

# Run the container
docker run -p 8080:8080 dotnet-webapi
```

## CI/CD Pipeline

The GitHub Actions pipeline consists of two main jobs:

### 1. Build & Test Job
- Runs on all branches and pull requests
- Sets up .NET 8 environment
- Restores dependencies, builds, and runs tests
- Uploads test results and build artifacts

### 2. Docker Build & Push Job
- Runs only on `main`/`master` branch or manual trigger
- Builds Alpine-based Docker image
- Pushes to JFrog Artifactory with version tags
- Publishes build information

## Configuration

### Required GitHub Secrets

Set these in your repository settings (`Settings > Secrets and variables > Actions`):

**Variables:**
- `JF_URL`: Your JFrog instance URL (e.g., `https://yourcompany.jfrog.io`)

**Secrets:**
- `JF_ACCESS_TOKEN`: JFrog access token with appropriate permissions

### JFrog Artifactory Setup

1. Create a Docker repository named `images-local`
2. Generate an access token with read/write permissions
3. Update the `REGISTRY_PATH` in the pipeline if using a different repository

## API Endpoints

- `GET /weatherforecast` - Returns sample weather forecast data
- `GET /swagger` - API documentation (development environment)

## Technologies Used

- **.NET 8**: Latest LTS version of .NET
- **ASP.NET Core**: Web API framework
- **Alpine Linux**: Minimal container base image
- **GitHub Actions**: CI/CD automation
- **JFrog Artifactory**: Container registry
- **xUnit**: Testing framework
- **Swagger/OpenAPI**: API documentation

## Docker Image Details

- **Base Image**: `mcr.microsoft.com/dotnet/aspnet:8.0-alpine`
- **Size**: ~181MB
- **Security**: Runs as non-root user (`app`)
- **Globalization**: ICU libraries included for internationalization

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Run tests locally
5. Submit a pull request

## License

This project is a POC for demonstration purposes.
