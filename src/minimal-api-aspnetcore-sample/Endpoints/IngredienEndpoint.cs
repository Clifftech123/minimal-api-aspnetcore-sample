using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Services;

namespace minimal_api_aspnetcore_sample.Endpoints
{
    public static class IngredientEndpoints
    {
        public static IEndpointRouteBuilder MapIngredientEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapPost("/ingredient", CreateIngredient);
            app.MapPut("/ingredient/{id:int}", UpdateIngredient);
            app.MapDelete("/ingredient/{id:int}", DeleteIngredient);
            app.MapGet("/ingredient", GetIngredientsAsync);
            app.MapGet("/ingredient/{id:int}", GetIngredientById);

            return app;
        }


        // create ingredient
        public static async Task<IResult> CreateIngredient(CreateIngredientRequest request, IngredientServices ingredientService)
        {
            var createdIngredient = await ingredientService.CreateIngredientAsync(request);
            return Results.Created($"/ingredient/{createdIngredient.Id}", createdIngredient);
        }


        // Update ingredient by id

        public static async Task<IResult> UpdateIngredient(int id, CreateIngredientRequest request, IngredientServices ingredientService)
        {
            var updatedIngredient = await ingredientService.UpdateIngredientAsync(id, request);
            return updatedIngredient != null ? Results.Ok(updatedIngredient) : Results.NotFound();
        }

        // delete ingredient by id

        public static async Task<IResult> DeleteIngredient(int id, IngredientServices ingredientService)
        {
            var success = await ingredientService.DeleteIngredientAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        }

        // get all ingredients

        public static async Task<IResult> GetIngredientsAsync(IngredientServices ingredientService)
        {
            var ingredients = await ingredientService.GetIngredientsAsync();
            return Results.Ok(ingredients);
        }


        // get ingredient by id
        public static async Task<IResult> GetIngredientById(int id, IngredientServices ingredientService)
        {
            var ingredient = await ingredientService.GetIngredientByIdAsync(id);
            return ingredient != null ? Results.Ok(ingredient) : Results.NotFound();
        }
    }
}
