namespace minimal_api_aspnetcore_sample.Infrastructure.Exceptions
{
    public class IngredientDoesNotExistException : Exception
    {
        public int Id { get; }

        public IngredientDoesNotExistException(int id) : base($"Ingredient with ID {id} not found")
        {
            Id = id;
        }
    }
}
