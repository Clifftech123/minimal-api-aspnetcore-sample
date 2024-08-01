namespace minimal_api_aspnetcore_sample.Models
{
    public class Instruction
    {
        public const string TableName = "Instructions";
        public int Id { get; set; }
        public string Description { get; set; }
        public int StepNumber { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
