namespace RecipeShare.Core.Models
{
    public class RecipeModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public string Steps { get; set; } = string.Empty;
        public int CookingTimeMinutes { get; set; }
        public List<string> DietaryTags { get; set; } = new();
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}