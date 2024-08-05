namespace minimal_api_aspnetcore_sample.Endpoints.Contracts
{
    public record CreateIngredientRequest(string Name, string Quantity, int RecipeId);

}