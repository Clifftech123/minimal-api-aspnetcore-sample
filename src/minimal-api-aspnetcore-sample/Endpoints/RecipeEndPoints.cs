using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Services;

namespace minimal_api_aspnetcore_sample.Endpoints
{
    public static class RecipeEndPoints
    {

      public  static IEndpointRouteBuilder MapRecipeEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/recipes", CreateRecipeAsync);
            app.MapGet("/recipes", GetRecipesAsync);
            app.MapGet("/recipes/{id:int}", GetRecipeAsync);
            app.MapPut("/recipes/{id:int}", UpdateRecipeAsync);
            app.MapPut("/recipe/{id:int}", DeleteRecipeAsync);



            return app;

        }


        // Create recipe
        public static async Task<IResult> CreateRecipeAsync(CreateRecipeRequest request, RecipeService recipeService)
        {
            var createdRecipe = await recipeService.CreateRecipeAsync(request);
            return Results.Created($"/recipes/{createdRecipe.Id}", createdRecipe);
        }

        // Get all recipes
        public static async Task<IResult> GetRecipesAsync(RecipeService recipeService)
        {
            var recipes = await recipeService.GetRecipesAsync();
            return Results.Ok(recipes);
        }


        // Get recipe by id
        public static async Task<IResult> GetRecipeAsync(int id, RecipeService recipeService)
        {
            var recipe = await recipeService.GetRecipeByIdAsync(id);
            return recipe != null ? Results.Ok(recipe) : Results.NotFound();
        }


        // Update recipe by id
        public static async Task<IResult> UpdateRecipeAsync(int id, CreateRecipeRequest request, RecipeService recipeService)
        {
            var updatedRecipe = await recipeService.UpdateRecipeAsync(id, request);
            return updatedRecipe != null ? Results.Ok(updatedRecipe) : Results.NotFound();
        }


        // Delete recipe by id

        public static async Task<IResult> DeleteRecipeAsync(int id, RecipeService recipeService)
        {
            var success = await recipeService.DeleteRecipeAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        }
    }
}
