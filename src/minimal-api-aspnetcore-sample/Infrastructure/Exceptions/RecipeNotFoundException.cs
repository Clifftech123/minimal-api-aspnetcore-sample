namespace minimal_api_aspnetcore_sample.Infrastructure.Exceptions
{
    public class RecipeNotFoundException : Exception
    {
        public int Id { get; }

        public RecipeNotFoundException(int id) : base($"Recipe with ID {id} not found")
        {
            Id = id;
        }
    }
}
