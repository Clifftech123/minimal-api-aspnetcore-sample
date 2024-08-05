using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Services;

namespace minimal_api_aspnetcore_sample.Endpoints
{
    public static class RecipeEndPoints
    {
        /// <summary>
        /// Maps the recipe endpoints.
        /// </summary>
        /// <param name="app">The endpoint route builder.</param>
        /// <returns>The endpoint route builder with mapped endpoints.</returns>
        public static IEndpointRouteBuilder MapRecipeEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/recipes", CreateRecipeAsync);
            app.MapGet("/recipes", GetRecipesAsync);
            app.MapGet("/recipes/{id:int}", GetRecipeAsync);
            app.MapPut("/recipes/{id:int}", UpdateRecipeAsync);
            app.MapPut("/recipe/{id:int}", DeleteRecipeAsync);

            return app;
        }

        /// <summary>
        /// Creates a new recipe.
        /// </summary>
        /// <param name="request">The create recipe request.</param>
        /// <param name="recipeService">The recipe service.</param>
        /// <returns>The created recipe.</returns>
        public static async Task<IResult> CreateRecipeAsync(CreateRecipeRequest request, RecipeService recipeService)
        {
            var createdRecipe = await recipeService.CreateRecipeAsync(request);
            return Results.Created($"/recipes/{createdRecipe.Id}", createdRecipe);
        }

        /// <summary>
        /// Gets all recipes.
        /// </summary>
        /// <param name="recipeService">The recipe service.</param>
        /// <returns>A list of recipes.</returns>
        public static async Task<IResult> GetRecipesAsync(RecipeService recipeService)
        {
            var recipes = await recipeService.GetRecipesAsync();
            return Results.Ok(recipes);
        }

        /// <summary>
        /// Gets a recipe by ID.
        /// </summary>
        /// <param name="id">The recipe ID.</param>
        /// <param name="recipeService">The recipe service.</param>
        /// <returns>The recipe with the specified ID.</returns>
        public static async Task<IResult> GetRecipeAsync(int id, RecipeService recipeService)
        {
            var recipe = await recipeService.GetRecipeByIdAsync(id);
            return recipe != null ? Results.Ok(recipe) : Results.NotFound();
        }

        /// <summary>
        /// Updates a recipe by ID.
        /// </summary>
        /// <param name="id">The recipe ID.</param>
        /// <param name="request">The update recipe request.</param>
        /// <param name="recipeService">The recipe service.</param>
        /// <returns>The updated recipe.</returns>
        public static async Task<IResult> UpdateRecipeAsync(int id, CreateRecipeRequest request, RecipeService recipeService)
        {
            var updatedRecipe = await recipeService.UpdateRecipeAsync(id, request);
            return updatedRecipe != null ? Results.Ok(updatedRecipe) : Results.NotFound();
        }

        /// <summary>
        /// Deletes a recipe by ID.
        /// </summary>
        /// <param name="id">The recipe ID.</param>
        /// <param name="recipeService">The recipe service.</param>
        /// <returns>No content if the recipe was deleted, otherwise not found.</returns>
        public static async Task<IResult> DeleteRecipeAsync(int id, RecipeService recipeService)
        {
            var success = await recipeService.DeleteRecipeAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        }
    }
}
