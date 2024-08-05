using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Services;

namespace minimal_api_aspnetcore_sample.Endpoints
{
    public static class InstructionEndpoints
    {
        /// <summary>
        /// Maps the instruction endpoints.
        /// </summary>
        /// <param name="app">The endpoint route builder.</param>
        /// <returns>The endpoint route builder with mapped endpoints.</returns>
        public static IEndpointRouteBuilder MapInstructionEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/instructions", CreateInstruction);
            app.MapPut("/instructions/{id:int}", UpdateInstruction);
            app.MapDelete("/instructions/{id:int}", DeleteInstruction);
            app.MapGet("/instructions", GetInstructions);
            app.MapGet("/instructions/{id:int}", GetInstructionById);

            return app;
        }

        /// <summary>
        /// Creates a new instruction.
        /// </summary>
        /// <param name="request">The create instruction request.</param>
        /// <param name="instructionsService">The instruction service.</param>
        /// <returns>The created instruction.</returns>
        public static async Task<IResult> CreateInstruction(CreateInstructionRequest request, InstructionServices instructionsService)
        {
            var createdInstruction = await instructionsService.CreateInstruction(request);
            return Results.Created($"/instructions/{createdInstruction.Id}", createdInstruction);
        }

        /// <summary>
        /// Updates an instruction by ID.
        /// </summary>
        /// <param name="id">The instruction ID.</param>
        /// <param name="request">The update instruction request.</param>
        /// <param name="instructionsService">The instruction service.</param>
        /// <returns>The updated instruction.</returns>
        public static async Task<IResult> UpdateInstruction(int id, CreateInstructionRequest request, InstructionServices instructionsService)
        {
            var updatedInstruction = await instructionsService.UpdateInstruction(id, request);
            return updatedInstruction != null ? Results.Ok(updatedInstruction) : Results.NotFound();
        }

        /// <summary>
        /// Deletes an instruction by ID.
        /// </summary>
        /// <param name="id">The instruction ID.</param>
        /// <param name="instructionsService">The instruction service.</param>
        /// <returns>No content if the instruction was deleted, otherwise not found.</returns>
        public static async Task<IResult> DeleteInstruction(int id, InstructionServices instructionsService)
        {
            var success = await instructionsService.DeleteInstruction(id);
            return success ? Results.NoContent() : Results.NotFound();
        }

        /// <summary>
        /// Gets all instructions.
        /// </summary>
        /// <param name="instructionsService">The instruction service.</param>
        /// <returns>A list of instructions.</returns>
        public static async Task<IResult> GetInstructions(InstructionServices instructionsService)
        {
            var instructions = await instructionsService.GetInstructions();
            return Results.Ok(instructions);
        }

        /// <summary>
        /// Gets an instruction by ID.
        /// </summary>
        /// <param name="id">The instruction ID.</param>
        /// <param name="instructionsService">The instruction service.</param>
        /// <returns>The instruction with the specified ID.</returns>
        public static async Task<IResult> GetInstructionById(int id, InstructionServices instructionsService)
        {
            var instruction = await instructionsService.GetInstructionById(id);
            return instruction != null ? Results.Ok(instruction) : Results.NotFound();
        }
    }
}
