using Microsoft.AspNetCore.Mvc;
using RecipeShare.Core.Models;
using RecipeShare.Core.Entities;
using RecipeShare.Core.Interfaces;

namespace RecipeShare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _repository;
        private readonly ILogger<RecipesController> _logger;
        
        public RecipesController(IRecipeRepository repository, ILogger<RecipesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeModel>>> GetRecipes()
        {
            var recipes = await _repository.GetAllAsync();
            var recipeModels = recipes.Select(MapToModel);
            return Ok(recipeModels);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeModel>> GetRecipe(Guid id)
        {
            var recipe = await _repository.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();
                
            return Ok(MapToModel(recipe));
        }
        
        [HttpPost]
        public async Task<ActionResult<RecipeModel>> CreateRecipe(CreateRecipeModel createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var recipe = MapToEntity(createModel);
            var created = await _repository.CreateAsync(recipe);
            
            _logger.LogInformation("Created recipe {RecipeId} - {Title}", created.Id, created.Title);
            
            return CreatedAtAction(nameof(GetRecipe), new { id = created.Id }, MapToModel(created));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeModel>> UpdateRecipe(Guid id, CreateRecipeModel updateModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var existingRecipe = await _repository.GetByIdAsync(id);
            if (existingRecipe == null)
                return NotFound();
                
            existingRecipe.Title = updateModel.Title;
            existingRecipe.Ingredients = updateModel.Ingredients;
            existingRecipe.Steps = updateModel.Steps;
            existingRecipe.CookingTimeMinutes = updateModel.CookingTimeMinutes;
            existingRecipe.DietaryTags = updateModel.DietaryTags;
            
            var updated = await _repository.UpdateAsync(existingRecipe);
            
            _logger.LogInformation("Updated recipe {RecipeId}", id);
            
            return Ok(MapToModel(updated));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await _repository.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();
                
            await _repository.DeleteAsync(id);
            
            _logger.LogInformation("Deleted recipe {RecipeId}", id);
            
            return NoContent();
        }
        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RecipeModel>>> SearchRecipes(
            [FromQuery] List<string>? dietaryTags,
            [FromQuery] int? maxCookingTime)
        {
            var recipes = await _repository.SearchAsync(dietaryTags, maxCookingTime);
            var recipeModels = recipes.Select(MapToModel);
            return Ok(recipeModels);
        }
        
        private static RecipeModel MapToModel(Recipe recipe) => new()
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Ingredients = recipe.Ingredients,
            Steps = recipe.Steps,
            CookingTimeMinutes = recipe.CookingTimeMinutes,
            DietaryTags = recipe.DietaryTags,
            LastUpdated = recipe.LastUpdated,
            CreatedAt = recipe.CreatedAt
        };
        
        private static Recipe MapToEntity(CreateRecipeModel Model) => new()
        {
            Title = Model.Title,
            Ingredients = Model.Ingredients,
            Steps = Model.Steps,
            CookingTimeMinutes = Model.CookingTimeMinutes,
            DietaryTags = Model.DietaryTags
        };
    }
}