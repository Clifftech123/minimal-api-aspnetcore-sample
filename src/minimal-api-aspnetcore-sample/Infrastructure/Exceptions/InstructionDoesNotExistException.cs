namespace minimal_api_aspnetcore_sample.Infrastructure.Exceptions
{
    public class InstructionDoesNotExistException : Exception
    {
        public int Id { get; }

        public InstructionDoesNotExistException(int id) : base($"Instruction with ID {id} not found")
        {
            Id = id;
        }
    }
}
