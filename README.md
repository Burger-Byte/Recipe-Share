# Recipe-Share

Welcome to RecipeShare, a vibrant community where home cooks and food bloggers discover, create, and share delicious recipes.
A modern recipe sharing platform built with .NET 8.0, Entity Framework Core, and SQL Server. Supports deployment with both Docker Compose and Kubernetes.

## Features

- **Recipe Management**: Create, read, update, and delete recipes
- **Search & Filter**: Find recipes by dietary tags, cooking time, and more
- **Community Driven**: Share your favorite recipes with fellow food enthusiasts
- **Modern Architecture**: Built with ASP.NET 8, Entity Framework Core, and containerized for easy deployment

## 🏗️ Architecture Overview

### Technology Stack
- **Backend**: .NET 8.0 Web API
- **Database**: SQL Server 2022 Express
- **ORM**: Entity Framework Core
- **Containerization**: Docker
- **Orchestration**: Docker Compose / Kubernetes
- **CI/CD**: GitHub Actions

### Deployment Options
- **Docker Compose**: Simple local development and testing
- **Kubernetes**: Production-ready container orchestration
- **Local Development**: Direct .NET development with containerized database


## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server or PostgreSQL
- Docker


### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running
- [Git](https://git-scm.com/) for cloning the repository
- [kubectl](https://kubernetes.io/docs/tasks/tools/) (for Kubernetes deployment)

### 1. Clone and Setup
```bash
git clone https://github.com/Burger-Byte/Recipe-Share.git
cd Recipe-Share

# Copy environment template
cp .env.example .env

# Edit .env with your values (especially SQL_PASSWORD)
```

### 2. Choose Your Deployment Method

#### Option A: Docker Compose (Recommended for Development)
```bash
docker compose up -d --build
```

**Access Points:**
- API: http://localhost:8080
- Swagger UI: http://localhost:8080/swagger
- Health Check: http://localhost:8080/healthz

#### Option B: Kubernetes (Production-like)
```bash
# Create secrets from environment file
kubectl create secret generic recipeshare-secrets --from-env-file=.env

# Deploy to Kubernetes
kubectl apply -f k8s-local.yaml

# Wait for pods to be ready
kubectl get pods -w
```

**Access Points:**
- API: http://localhost:30080
- Swagger UI: http://localhost:30080/swagger
- Health Check: http://localhost:30080/healthz

## 📋 Environment Configuration

### Required Environment Variables

Create a `.env` file in the root directory with these variables:

```bash
# Database Configuration
SQL_USERNAME=SomeCoolUserName
SQL_PASSWORD=SomeCoolPasswordHere123!
DATABASE_NAME=RecipeShareDb

# Application Configuration
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080

# Docker Configuration
DOCKER_REGISTRY=your-dockerhub-username
DOCKER_IMAGE_NAME=recipeshare-api
DOCKER_TAG=latest

# Kubernetes Configuration
K8S_NAMESPACE=default

# Connection Strings (Auto-configured - don't modify these)
CONNECTION_STRING_DOCKER=Server=recipeshare-db,1433;Database=RecipeShareDb;User Id=SomeCoolUserName;Password=SomeCoolPasswordHere123!;TrustServerCertificate=True;MultipleActiveResultSets=true
CONNECTION_STRING_K8S=Server=sqlserver-service,1433;Database=RecipeShareDb;User Id=SomeCoolUserName;Password=SomeCoolPasswordHere123!;TrustServerCertificate=True;MultipleActiveResultSets=true
CONNECTION_STRING_LOCAL=Server=localhost,1433;Database=RecipeShareDb;User Id=SomeCoolUserName;Password=SomeCoolPasswordHere123!;TrustServerCertificate=True;MultipleActiveResultSets=true
```

### Security Notes
- **Never commit `.env` to version control** - it's already in `.gitignore`
- Use strong passwords for `SQL_PASSWORD`
- For production, use a proper secret management system for sensitive data like Github Secrets, Azure Key Vault, etc.s


### CRUD API Endpoints

- `POST /api/recipes` - Create new recipe
- `GET /api/recipes/search` - Search recipes with filters
- `PUT /api/recipes/:id` - Update existing recipe
- `DELETE /api/recipes/:id` - Delete recipe


## 🐳 Docker Compose Deployment

### Development Workflow
```bash
# Start services in background
docker compose up -d

# View logs
docker compose logs -f

# Stop services
docker compose down

# Rebuild and restart
docker compose up -d --build

# Remove everything including data
docker compose down -v
```

### Services
- **recipeshare-db**: SQL Server 2022 Express database
- **recipeshare-api**: .NET 8.0 Web API application

### Volumes
- **recipeshare_data**: Persistent database storage

## ☸️ Kubernetes Deployment

### Prerequisites
```bash
# Verify kubectl is connected to a cluster
kubectl cluster-info

# For local development, ensure Docker Desktop Kubernetes is enabled
```

### Deployment Steps

1. **Create Secrets:**
   ```bash
   kubectl create secret generic recipeshare-secrets --from-env-file=.env
   ```

2. **Deploy Application:**
   ```bash
   kubectl apply -f k8s-local.yaml
   ```

3. **Verify Deployment:**
   ```bash
   kubectl get pods,services,secrets
   ```

4. **Check Application Logs:**
   ```bash
   kubectl logs -l app=recipeshare-api -f
   ```

### Kubernetes Resources

- **ConfigMap**: `recipeshare-config` - Non-sensitive configuration
- **Secret**: `recipeshare-secrets` - Database credentials and connection strings
- **PVC**: `sqlserver-pvc` - Persistent storage for database
- **Deployments**: 
  - `sqlserver` - SQL Server database (1 replica)
  - `recipeshare-api` - Web API application (2 replicas)
- **Services**:
  - `sqlserver-service` - Internal database access
  - `recipeshare-api-service` - External API access (NodePort 30080)

### Scaling
```bash
# Scale API instances
kubectl scale deployment recipeshare-api --replicas=3

# Check scaled deployment
kubectl get pods -l app=recipeshare-api
```

## 🔧 Local Development

### Running API Locally with Containerized Database
```bash
# Start only the database
docker compose up -d recipeshare-db

# Run the API locally
dotnet run --project src/RecipeShare.Api

# Access at http://localhost:5000 or http://localhost:5001 (HTTPS)
```

### Database Management
```bash
# Access SQL Server container
docker exec -it recipeshare-sqlserver /bin/bash

# Connect with sqlcmd (if available in container)
sqlcmd -S localhost -U SomeCoolUserName -P SomeCoolPasswordHere123!
```

## 🚦 Health Checks and Monitoring

### Health Endpoints
- **Health Check**: `/healthz` - Application health status
- **Swagger UI**: `/swagger` - Interactive API documentation

### Docker Compose Health Checks
- **Database**: SQL connectivity test
- **API**: HTTP health endpoint verification

### Kubernetes Health Checks
- **Liveness Probe**: Ensures containers are running
- **Readiness Probe**: Ensures containers are ready to serve traffic

### Monitoring Commands
```bash
# Docker Compose
docker compose ps
docker compose logs -f recipeshare-api

# Kubernetes
kubectl get pods
kubectl describe pod <pod-name>
kubectl logs -l app=recipeshare-api -f
```

## 🛠️ CI/CD Pipeline

### GitHub Actions Workflow

The repository includes a complete CI/CD pipeline that:

1. **Build & Test**: Compiles .NET application and runs tests
2. **Code Quality**: SonarCloud analysis for code quality and security
3. **Docker Build**: Creates and pushes Docker images to Docker Hub
4. **Security Scanning**: Trivy vulnerability scanning
5. **Deployment**: Simulated Kubernetes deployment

### Required GitHub Secrets
```bash
DOCKERHUB_USERNAME    # Docker Hub username
DOCKERHUB_TOKEN      # Docker Hub access token
SQL_USERNAME         # Database username (SomeCoolUserName)
SQL_PASSWORD         # Database password
SONAR_TOKEN          # SonarCloud token (optional)
```

### Workflow Triggers
- **Push to `main`**: Full pipeline with deployment
- **Push to `develop`**: Build and test only
- **Pull Requests**: Build, test, and security checks

## 🔍 API Documentation

### Swagger UI
Access interactive API documentation at:
- Docker Compose: http://localhost:8080/swagger
- Kubernetes: http://localhost:30080/swagger

### Available Endpoints
- `GET /healthz` - Health check endpoint
- Recipe management endpoints (documented in Swagger)

## 🐛 Troubleshooting

### Common Issues

#### Docker Compose Issues
```bash
# Container not starting
docker compose logs recipeshare-api

# Database connection issues
docker compose exec recipeshare-db sqlcmd -S localhost -U SomeCoolUserName -P SomeCoolPasswordHere123!

# Port conflicts
docker compose down
# Change ports in docker-compose.yaml if needed
```

#### Kubernetes Issues
```bash
# Pod not starting
kubectl describe pod <pod-name>
kubectl logs <pod-name>

# Image pull errors
kubectl describe pod <pod-name>
# Check image name and registry settings

# Secret issues
kubectl get secret recipeshare-secrets -o yaml
kubectl delete secret recipeshare-secrets
kubectl create secret generic recipeshare-secrets --from-env-file=.env
```

#### Database Connection Issues
- Verify passwords match in `.env` file
- Check SQL Server is fully started (look for "ready for client connections" in logs)
- Ensure connection strings use correct service names

### Debug Commands
```bash
# Test database connectivity
kubectl run test-sql --rm -it --image=mcr.microsoft.com/mssql-tools --restart=Never -- /opt/mssql-tools/bin/sqlcmd -S sqlserver-service -U SomeCoolUserName -P SomeCoolPasswordHere123! -Q "SELECT 1"

# Check service endpoints
kubectl get endpoints

# Port forwarding for debugging
kubectl port-forward svc/recipeshare-api-service 8080:80
```

## 📁 Project Structure

```
Recipe-Share/
├── .github/workflows/          # CI/CD pipeline
├── src/                        # Source code
│   ├── RecipeShare.Api/        # Web API project
│   ├── RecipeShare.Core/       # Business logic
│   └── RecipeShare.Infrastructure/ # Data access
├── tests/                      # Test projects
├── docker-compose.yaml         # Docker Compose configuration
├── k8s-local.yaml             # Kubernetes manifests
├── Dockerfile                 # Container build instructions
├── .env.example               # Environment template
└── README.md                  # This file
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Test with both Docker Compose and Kubernetes
5. Commit your changes (`git commit -m 'Add amazing feature'`)
6. Push to the branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

- **Issues**: Create an issue in the GitHub repository
- **Documentation**: Check this README and the `/docs` folder
- **CI/CD**: Check GitHub Actions for build status

---

**Happy Cooking! 🍳**