namespace minimal_api_aspnetcore_sample.Endpoints.Contracts
{
    public class RecipeResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<IngredientResponse> Ingredients { get; set; }
        public List<InstructionResponse> Instructions { get; set; }
    }
}
