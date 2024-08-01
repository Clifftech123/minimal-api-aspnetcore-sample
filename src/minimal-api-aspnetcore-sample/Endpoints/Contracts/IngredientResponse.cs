namespace minimal_api_aspnetcore_sample.Endpoints.Contracts
{

    public record IngredientResponse (int Id, string Name, string Quantity, int RecipeId);
   
}


