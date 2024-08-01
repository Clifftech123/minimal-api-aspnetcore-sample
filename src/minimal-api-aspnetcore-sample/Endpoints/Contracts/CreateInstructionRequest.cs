namespace minimal_api_aspnetcore_sample.Endpoints.Contracts
{
    public  record CreateInstructionRequest (string Description, int StepNumber, int RecipeId);
    
}
