using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Services;

namespace minimal_api_aspnetcore_sample.Endpoints
{
    public static class InstructionEndpoints
    {

        public static IEndpointRouteBuilder MapInstructionEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapPost("/instructions", CreateInstruction);
            app.MapPut("/instructions/{id:int}", UpdateInstruction);
            app.MapDelete("/instructions/{id:int}", DeleteInstruction);
            app.MapGet("/instructions", GetInstructions);
            app.MapGet("/instructions/{id:int}", GetInstructionById);

            return app;
        }

        // create instruction
        public static async Task<IResult> CreateInstruction(CreateInstructionRequest request, InstructionServices instructionsService)
        {
            var createdInstruction = await instructionsService.CreateInstruction(request);
            return Results.Created($"/instructions/{createdInstruction.Id}", createdInstruction);
        }

        // update instruction by id
        public static async Task<IResult> UpdateInstruction(int id, CreateInstructionRequest request, InstructionServices instructionsService)
        {
            var updatedInstruction = await instructionsService.UpdateInstruction(id, request);
            return updatedInstruction != null ? Results.Ok(updatedInstruction) : Results.NotFound();
        }


        // delete instruction by id
        public static async Task<IResult> DeleteInstruction(int id, InstructionServices instructionsService)
        {
            var success = await instructionsService.DeleteInstruction(id);
            return success ? Results.NoContent() : Results.NotFound();
        }


        // get all instructions

        public static async Task<IResult> GetInstructions(InstructionServices instructionsService)
        {
            var instructions = await instructionsService.GetInstructions();
            return Results.Ok(instructions);
        }


        // get instruction by id

        public static async Task<IResult> GetInstructionById(int id, InstructionServices instructionsService)
        {
            var instruction = await instructionsService.GetInstructionById(id);
            return instruction != null ? Results.Ok(instruction) : Results.NotFound();
        }
    }

}

