namespace minimal_api_aspnetcore_sample.Models
{
    public class Ingredient
    {
        public const string TableName = "Ingredients";
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

    }
}
