# Recipe-Share

Welcome to RecipeShare, a vibrant community where home cooks and food bloggers discover, create, and share delicious recipes.


## Features

- **Recipe Management**: Create, read, update, and delete recipes
- **Search & Filter**: Find recipes by dietary tags, cooking time, and more
- **Community Driven**: Share your favorite recipes with fellow food enthusiasts
- **Modern Architecture**: Built with ASP.NET 8, Entity Framework Core, and containerized for easy deployment


## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server or PostgreSQL
- Docker



### CRUD API Endpoints

- `POST /api/recipes` - Create new recipe
- `GET /api/recipes/search` - Search recipes with filters
- `PUT /api/recipes/:id` - Update existing recipe
- `DELETE /api/recipes/:id` - Delete recipe


### Health Monitoring

- Health checks available at `/healthz`

## Deployment

The application supports multiple deployment options:

- **Docker**: Use the included Dockerfile and docker-compose.yml
- **Kubernetes**: Deploy using the provided YAML manifests
- **CI/CD**: Automated pipeline with Github Actions or Azure DevOps

