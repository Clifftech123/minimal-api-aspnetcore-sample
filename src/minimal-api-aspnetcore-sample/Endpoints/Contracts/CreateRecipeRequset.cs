namespace minimal_api_aspnetcore_sample.Endpoints.Contracts
{
    public record CreateRecipeRequset 
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<int> IngredientIds { get; set; }
        public List<int> InstructionIds { get; set; }
    }
}
