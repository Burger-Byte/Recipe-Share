using RecipeShare.Core.Entities;

namespace RecipeShare.Core.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(Guid id);
        Task<Recipe> CreateAsync(Recipe recipe);
        Task<Recipe> UpdateAsync(Recipe recipe);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Recipe>> SearchAsync(List<string>? dietaryTags, int? maxCookingTime);
    }
}