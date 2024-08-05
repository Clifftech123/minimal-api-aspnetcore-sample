using Microsoft.EntityFrameworkCore;
using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Infrastructure.Context;
using minimal_api_aspnetcore_sample.Infrastructure.Exceptions;
using minimal_api_aspnetcore_sample.Models;


namespace minimal_api_aspnetcore_sample.Services
{
    public class RecipeService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<RecipeService> _logger;

        public RecipeService(ApplicationContext context, ILogger<RecipeService> logger)
        {
            _context = context;
            _logger = logger;
        }


        // Creating a new recipe
        public async Task<RecipeResponse> CreateRecipeAsync(CreateRecipeRequest request)
        {
            _logger.LogInformation("Creating a new recipe");
            var ingredients = await _context.Ingredients
                .Where(i => request.IngredientIds.Contains(i.Id))
                .ToListAsync();

            var instructions = await _context.Instructions
                .Where(i => request.InstructionIds.Contains(i.Id))
                .ToListAsync();

            var recipe = new Recipe
            {

                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                Ingredients = ingredients,
                Instructions = instructions
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Recipe created successfully");
            return new RecipeResponse
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Category = recipe.Category,
                Ingredients = recipe.Ingredients.Select(i => new IngredientResponse(
                    i.Id,
                    i.Name,
                    i.Quantity,
                    i.RecipeId
                )).ToList(),
                Instructions = recipe.Instructions.Select(i => new InstructionResponse(
                    i.Id,
                    i.Description,
                    i.StepNumber,
                    i.RecipeId
                )).ToList()
            };
        }

        // Get all recipes
        public async Task<IEnumerable<RecipeResponse>> GetRecipesAsync()
        {
            _logger.LogInformation("Getting all recipes");

            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .ToListAsync();

            return recipes.Select(recipe => new RecipeResponse
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Category = recipe.Category,
                Ingredients = recipe.Ingredients.Select(i => new IngredientResponse(
                    i.Id,
                    i.Name,
                    i.Quantity,
                    i.RecipeId
                )).ToList(),
                Instructions = recipe.Instructions.Select(i => new InstructionResponse(
                    i.Id,
                    i.Description,
                    i.StepNumber,
                    i.RecipeId
                )).ToList()
            });
        }



        // Get recipe by id

        public async Task<RecipeResponse> GetRecipeByIdAsync(int id)
        {
            _logger.LogInformation("Getting recipe by ID");

            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                _logger.LogError("Recipe with ID {RecipeId} not found", id);
                throw new RecipeNotFoundException(id);
            }

            return new RecipeResponse
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Category = recipe.Category,
                Ingredients = recipe.Ingredients.Select(i => new IngredientResponse(
                    i.Id,
                    i.Name,
                    i.Quantity,
                    i.RecipeId
                )).ToList(),
                Instructions = recipe.Instructions.Select(i => new InstructionResponse(
                    i.Id,
                    i.Description,
                    i.StepNumber,
                    i.RecipeId
                )).ToList()
            };
        }


        // Update recipe by id


        public async Task<RecipeResponse> UpdateRecipeAsync(int id, CreateRecipeRequest request)
        {
            _logger.LogInformation("Updating recipe by ID");
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                throw new RecipeNotFoundException(id);
            }

            _logger.LogInformation("Getting ingredients and instructions by ID");
            var ingredients = await _context.Ingredients
                .Where(i => request.IngredientIds.Contains(i.Id))
                .ToListAsync();

            _logger.LogInformation("Getting instructions by ID");
            var instructions = await _context.Instructions
                .Where(i => request.InstructionIds.Contains(i.Id))
                .ToListAsync();

            recipe.Title = request.Title;
            recipe.Description = request.Description;
            recipe.Category = request.Category;
            recipe.Ingredients = ingredients;
            recipe.Instructions = instructions;

            _logger.LogInformation("Updating recipe");
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return new RecipeResponse
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Category = recipe.Category,
                Ingredients = recipe.Ingredients.Select(i => new IngredientResponse(
                    i.Id,
                    i.Name,
                    i.Quantity,
                    i.RecipeId
                )).ToList(),
                Instructions = recipe.Instructions.Select(i => new InstructionResponse(
                    i.Id,
                    i.Description,
                    i.StepNumber,
                    i.RecipeId
                )).ToList()
            };
        }




        // Delete recipe by id 

        public async Task<bool> DeleteRecipeAsync(int id)
        {
          
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new RecipeNotFoundException(id);
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
