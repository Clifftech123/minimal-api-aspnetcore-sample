using Microsoft.EntityFrameworkCore;
using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Infrastructure.Context;
using minimal_api_aspnetcore_sample.Infrastructure.Exceptions;
using minimal_api_aspnetcore_sample.Models;

namespace minimal_api_aspnetcore_sample.Services
{
    public class InstructionServices
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<InstructionServices> _logger;

        public InstructionServices(ApplicationContext context, ILogger<InstructionServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        //  creating a new instruction
        public async Task<InstructionResponse> CreateInstruction(CreateInstructionRequest request)
        {
            _logger.LogInformation("Creating a new instruction");
            var instruction = new Instruction
            {
                Description = request.Description,
                StepNumber = request.StepNumber,
                RecipeId = request.RecipeId
            };


            _logger.LogInformation("Adding instruction to the database");
            _context.Instructions.Add(instruction);
            await _context.SaveChangesAsync();

            var response = new InstructionResponse(
                instruction.Id,
                instruction.Description,
                instruction.StepNumber,
                instruction.RecipeId
            );

            _logger.LogInformation("Instruction created successfully");
            return response;
        }


        // get  all instructions

        public async Task<IEnumerable<InstructionResponse>> GetInstructions()
        {

            var instructions = await _context.Instructions.ToListAsync();

            return instructions.Select(instruction => new InstructionResponse(
                instruction.Id,
                instruction.Description,
                instruction.StepNumber,
                instruction.RecipeId
            ));
        }



        // get instruction by id

        public async Task<InstructionResponse> GetInstructionById(int id)
        {
            _logger.LogInformation("Getting instruction by ID");
            var instruction = await _context.Instructions.FindAsync(id);

            if (instruction == null)
            {
                _logger.LogError($"Instruction with ID {id} not found");
                throw new InstructionDoesNotExistException(id);
            }

            return new InstructionResponse(
                instruction.Id,
                instruction.Description,
                instruction.StepNumber,
                instruction.RecipeId
            );
        }



        // update instruction by id

        public async Task<InstructionResponse> UpdateInstruction(int id, CreateInstructionRequest request)
        {
            _logger.LogInformation("Updating instruction by ID");
            var instruction = await _context.Instructions.FindAsync(id);

            if (instruction == null)
            {
                _logger.LogError($"Instruction with ID {id} not found");
                throw new InstructionDoesNotExistException(id);
            }

            instruction.Description = request.Description;
            instruction.StepNumber = request.StepNumber;
            instruction.RecipeId = request.RecipeId;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Instruction updated successfully");
            return new InstructionResponse(
                instruction.Id,
                instruction.Description,
                instruction.StepNumber,
                instruction.RecipeId
            );
        }



        // delete instruction by id
        public async Task<bool> DeleteInstruction(int id)
        {
            _logger.LogInformation("Deleting instruction by ID");
            var instruction = await _context.Instructions.FindAsync(id);

            if (instruction == null)
            {
                _logger.LogError($"Instruction with ID {id} not found");
                throw new InstructionDoesNotExistException(id);
            }

            _context.Instructions.Remove(instruction);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Instruction deleted successfully");
            return true;
        }



    }
}
