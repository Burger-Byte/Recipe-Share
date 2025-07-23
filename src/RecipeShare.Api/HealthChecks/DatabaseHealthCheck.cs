using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Infrastructure.Data;

namespace RecipeShare.Api.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly RecipeDbContext _context;

        public DatabaseHealthCheck(RecipeDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
                
                if (canConnect)
                {
                    var count = await _context.Recipes.CountAsync(cancellationToken);
                    return HealthCheckResult.Healthy($"Database OK. Recipes: {count}");
                }
                
                return HealthCheckResult.Unhealthy("Cannot connect to database");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database error", ex);
            }
        }
    }
}