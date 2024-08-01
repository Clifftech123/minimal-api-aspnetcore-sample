namespace minimal_api_aspnetcore_sample.Models
{

    public class Recipe
    {

        public const string TableName = "Recipes";
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }
        public string Category { get; set; }
    }

}

