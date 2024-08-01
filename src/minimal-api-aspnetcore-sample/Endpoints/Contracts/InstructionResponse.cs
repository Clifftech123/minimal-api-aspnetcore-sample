namespace minimal_api_aspnetcore_sample.Endpoints.Contracts
{

    public  record InstructionResponse (int Id, string Description, int StepNumber, int RecipeId);
    
}
