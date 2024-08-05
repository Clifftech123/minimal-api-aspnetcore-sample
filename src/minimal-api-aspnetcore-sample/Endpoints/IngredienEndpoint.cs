using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Services;

namespace minimal_api_aspnetcore_sample.Endpoints
{
    public static class IngredientEndpoints
    {
        /// <summary>
        /// Maps the ingredient endpoints.
        /// </summary>
        /// <param name="app">The endpoint route builder.</param>
        /// <returns>The endpoint route builder with mapped endpoints.</returns>
        public static IEndpointRouteBuilder MapIngredientEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/ingredient", CreateIngredient);
            app.MapPut("/ingredient/{id:int}", UpdateIngredient);
            app.MapDelete("/ingredient/{id:int}", DeleteIngredient);
            app.MapGet("/ingredient", GetIngredientsAsync);
            app.MapGet("/ingredient/{id:int}", GetIngredientById);

            return app;
        }

        /// <summary>
        /// Creates a new ingredient.
        /// </summary>
        /// <param name="request">The create ingredient request.</param>
        /// <param name="ingredientService">The ingredient service.</param>
        /// <returns>The created ingredient.</returns>
        public static async Task<IResult> CreateIngredient(CreateIngredientRequest request, IngredientServices ingredientService)
        {
            var createdIngredient = await ingredientService.CreateIngredientAsync(request);
            return Results.Created($"/ingredient/{createdIngredient.Id}", createdIngredient);
        }

        /// <summary>
        /// Updates an ingredient by ID.
        /// </summary>
        /// <param name="id">The ingredient ID.</param>
        /// <param name="request">The update ingredient request.</param>
        /// <param name="ingredientService">The ingredient service.</param>
        /// <returns>The updated ingredient.</returns>
        public static async Task<IResult> UpdateIngredient(int id, CreateIngredientRequest request, IngredientServices ingredientService)
        {
            var updatedIngredient = await ingredientService.UpdateIngredientAsync(id, request);
            return updatedIngredient != null ? Results.Ok(updatedIngredient) : Results.NotFound();
        }

        /// <summary>
        /// Deletes an ingredient by ID.
        /// </summary>
        /// <param name="id">The ingredient ID.</param>
        /// <param name="ingredientService">The ingredient service.</param>
        /// <returns>No content if the ingredient was deleted, otherwise not found.</returns>
        public static async Task<IResult> DeleteIngredient(int id, IngredientServices ingredientService)
        {
            var success = await ingredientService.DeleteIngredientAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        }

        /// <summary>
        /// Gets all ingredients.
        /// </summary>
        /// <param name="ingredientService">The ingredient service.</param>
        /// <returns>A list of ingredients.</returns>
        public static async Task<IResult> GetIngredientsAsync(IngredientServices ingredientService)
        {
            var ingredients = await ingredientService.GetIngredientsAsync();
            return Results.Ok(ingredients);
        }

        /// <summary>
        /// Gets an ingredient by ID.
        /// </summary>
        /// <param name="id">The ingredient ID.</param>
        /// <param name="ingredientService">The ingredient service.</param>
        /// <returns>The ingredient with the specified ID.</returns>
        public static async Task<IResult> GetIngredientById(int id, IngredientServices ingredientService)
        {
            var ingredient = await ingredientService.GetIngredientByIdAsync(id);
            return ingredient != null ? Results.Ok(ingredient) : Results.NotFound();
        }
    }
}
