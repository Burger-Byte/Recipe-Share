using Microsoft.EntityFrameworkCore;
using RecipeShare.Core.Entities;
using RecipeShare.Core.Interfaces;
using RecipeShare.Infrastructure.Data;

namespace RecipeShare.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _context;
        
        public RecipeRepository(RecipeDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes.ToListAsync();
        }
        
        public async Task<Recipe?> GetByIdAsync(Guid id)
        {
            return await _context.Recipes.FindAsync(id);
        }
        
        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }
        
        public async Task<Recipe> UpdateAsync(Recipe recipe)
        {
            recipe.LastUpdated = DateTime.UtcNow;
            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return recipe;
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<IEnumerable<Recipe>> SearchAsync(List<string>? dietaryTags, int? maxCookingTime)
        {
            var query = _context.Recipes.AsQueryable();
            
            if (dietaryTags != null && dietaryTags.Any())
            {
                query = query.Where(r => dietaryTags.Any(tag => r.DietaryTags.Contains(tag)));
            }
            
            if (maxCookingTime.HasValue)
            {
                query = query.Where(r => r.CookingTimeMinutes <= maxCookingTime.Value);
            }
            
            return await query.ToListAsync();
        }
    }
}