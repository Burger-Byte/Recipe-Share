using System.ComponentModel.DataAnnotations;

namespace RecipeShare.Core.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Ingredients { get; set; } = string.Empty;
        
        [Required]
        public string Steps { get; set; } = string.Empty;
        
        [Range(1, 1440)]
        public int CookingTimeMinutes { get; set; }
        
        public List<string> DietaryTags { get; set; } = new();
        
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}