using Microsoft.EntityFrameworkCore;
using RecipeShare.Core.Entities;
using System.Text.Json;

namespace RecipeShare.Infrastructure.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }
        
        public DbSet<Recipe> Recipes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DietaryTags)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                    );
            });
            
            SeedData(modelBuilder);
        }
        
        private void SeedData(ModelBuilder modelBuilder)
        {
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Classic Chocolate Chip Cookies",
                    Ingredients = "2 cups flour, 1 cup butter, 3/4 cup brown sugar, 1/2 cup white sugar, 2 eggs, 2 tsp vanilla, 1 tsp baking soda, 1 tsp salt, 2 cups chocolate chips",
                    Steps = "1. Preheat oven to 375°F. 2. Mix butter and sugars. 3. Add eggs and vanilla. 4. Combine dry ingredients. 5. Fold in chocolate chips. 6. Bake 9-11 minutes.",
                    CookingTimeMinutes = 25,
                    DietaryTags = new List<string> { "vegetarian", "dessert" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Quinoa Buddha Bowl",
                    Ingredients = "1 cup quinoa, 2 cups vegetable broth, 1 avocado, 1 cup chickpeas, 2 cups kale, 1/4 cup tahini, 2 tbsp lemon juice",
                    Steps = "1. Cook quinoa in vegetable broth. 2. Massage kale with olive oil. 3. Roast chickpeas. 4. Assemble bowl with quinoa, kale, chickpeas, avocado. 5. Drizzle with tahini dressing.",
                    CookingTimeMinutes = 30,
                    DietaryTags = new List<string> { "vegan", "gluten-free", "healthy" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Creamy Chicken Alfredo Pasta",
                    Ingredients = "1 lb fettuccine pasta, 2 chicken breasts, 1 cup heavy cream, 1/2 cup parmesan cheese, 4 cloves garlic, 4 tbsp butter, salt, pepper, parsley",
                    Steps = "1. Cook pasta according to package directions. 2. Season and cook chicken until golden. 3. Sauté garlic in butter. 4. Add cream and parmesan. 5. Slice chicken and serve over pasta with sauce.",
                    CookingTimeMinutes = 35,
                    DietaryTags = new List<string> { "comfort-food", "pasta" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Roasted Tomato Basil Soup",
                    Ingredients = "2 lbs tomatoes, 1 onion, 4 cloves garlic, 2 cups vegetable broth, 1/4 cup fresh basil, 2 tbsp olive oil, 1/4 cup heavy cream, salt, pepper",
                    Steps = "1. Roast tomatoes, onion, and garlic at 400°F for 30 minutes. 2. Blend with broth until smooth. 3. Simmer with basil for 10 minutes. 4. Stir in cream before serving.",
                    CookingTimeMinutes = 50,
                    DietaryTags = new List<string> { "vegetarian", "soup", "comfort-food" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Avocado Toast with Poached Eggs",
                    Ingredients = "2 slices whole grain bread, 1 ripe avocado, 2 eggs, 1 tbsp white vinegar, red pepper flakes, salt, pepper, lemon juice",
                    Steps = "1. Toast bread until golden. 2. Mash avocado with lemon juice, salt, and pepper. 3. Poach eggs in simmering water with vinegar. 4. Spread avocado on toast and top with poached eggs. 5. Season with red pepper flakes.",
                    CookingTimeMinutes = 15,
                    DietaryTags = new List<string> { "vegetarian", "breakfast", "healthy" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Mediterranean Greek Salad",
                    Ingredients = "2 large tomatoes, 1 cucumber, 1/2 red onion, 1/2 cup kalamata olives, 4 oz feta cheese, 3 tbsp olive oil, 1 tbsp red wine vinegar, 1 tsp oregano",
                    Steps = "1. Chop tomatoes, cucumber, and red onion. 2. Combine vegetables in large bowl. 3. Add olives and crumbled feta. 4. Whisk olive oil, vinegar, and oregano. 5. Toss salad with dressing.",
                    CookingTimeMinutes = 10,
                    DietaryTags = new List<string> { "vegetarian", "gluten-free", "salad", "mediterranean", "healthy" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Beef Stir-Fry with Vegetables",
                    Ingredients = "1 lb beef sirloin, 2 cups broccoli florets, 1 bell pepper, 1 carrot, 3 cloves garlic, 2 tbsp soy sauce, 1 tbsp cornstarch, 2 tbsp vegetable oil, 1 tsp ginger",
                    Steps = "1. Slice beef thinly and marinate in soy sauce and cornstarch. 2. Heat oil in wok over high heat. 3. Stir-fry beef until browned. 4. Add vegetables and garlic, stir-fry 3-4 minutes. 5. Add ginger and remaining soy sauce.",
                    CookingTimeMinutes = 20,
                    DietaryTags = new List<string> { "gluten-free", "healthy", "asian", "quick" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Baked Salmon with Lemon Herbs",
                    Ingredients = "4 salmon fillets, 2 lemons, 3 tbsp olive oil, 2 tbsp fresh dill, 2 tbsp fresh parsley, 3 cloves garlic, salt, pepper",
                    Steps = "1. Preheat oven to 425°F. 2. Mix olive oil, minced garlic, and herbs. 3. Season salmon with salt and pepper. 4. Top with herb mixture and lemon slices. 5. Bake for 12-15 minutes until flaky.",
                    CookingTimeMinutes = 25,
                    DietaryTags = new List<string> { "gluten-free", "healthy", "fish", "low-carb" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Vegan Lentil Curry",
                    Ingredients = "1 cup red lentils, 1 can coconut milk, 1 onion, 3 cloves garlic, 1 tbsp ginger, 2 tsp curry powder, 1 tsp turmeric, 1 can diced tomatoes, 2 cups vegetable broth",
                    Steps = "1. Sauté onion, garlic, and ginger until fragrant. 2. Add spices and cook 1 minute. 3. Add lentils, tomatoes, and broth. 4. Simmer 20 minutes until lentils are tender. 5. Stir in coconut milk and heat through.",
                    CookingTimeMinutes = 35,
                    DietaryTags = new List<string> { "vegan", "gluten-free", "healthy", "indian", "comfort-food" }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Classic Vanilla Cheesecake",
                    Ingredients = "1 1/2 cups graham cracker crumbs, 1/4 cup melted butter, 24 oz cream cheese, 3/4 cup sugar, 3 eggs, 1 tsp vanilla extract, 1/4 cup sour cream",
                    Steps = "1. Mix crumbs and butter, press into springform pan. 2. Beat cream cheese until smooth. 3. Add sugar, eggs, vanilla, and sour cream. 4. Pour over crust. 5. Bake at 325°F for 45 minutes. 6. Cool and refrigerate 4 hours.",
                    CookingTimeMinutes = 60,
                    DietaryTags = new List<string> { "vegetarian", "dessert", "baking" }
                }
            };
        
            modelBuilder.Entity<Recipe>().HasData(recipes);
        }
    }
}