using Microsoft.EntityFrameworkCore;
using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Infrastructure.Context;
using minimal_api_aspnetcore_sample.Infrastructure.Exceptions;
using minimal_api_aspnetcore_sample.Models;

namespace minimal_api_aspnetcore_sample.Services
{
    public class IngredientServices
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<IngredientServices> _logger;

        public IngredientServices(ApplicationContext context, ILogger<IngredientServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        //  creating a new ingredient
        public async Task<IngredientResponse> CreateIngredientAsync(CreateIngredientRequest request)
        {
            var ingredient = new Ingredient
            {
                Name = request.Name,
                Quantity = request.Quantity,
                RecipeId = request.RecipeId
            };

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return new IngredientResponse(
                ingredient.Id,
                ingredient.Name,
                ingredient.Quantity,
                ingredient.RecipeId
            );
        }

        // get  all ingredients
        public async Task<IEnumerable<IngredientResponse>> GetIngredientsAsync()
        {
            var ingredients = await _context.Ingredients.ToListAsync();

            return ingredients.Select(ingredient => new IngredientResponse(
                ingredient.Id,
                ingredient.Name,
                ingredient.Quantity,
                ingredient.RecipeId
            ));
        }


        // get ingredient by id

        public async Task<IngredientResponse> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                throw new IngredientDoesNotExistException(id);
            }

            return new IngredientResponse(
                ingredient.Id,
                ingredient.Name,
                ingredient.Quantity,
                ingredient.RecipeId
            );
        }
        public async Task<IngredientResponse?> UpdateIngredientAsync(int id, CreateIngredientRequest request)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                throw new IngredientDoesNotExistException(id);
            }

            ingredient.Name = request.Name;
            ingredient.Quantity = request.Quantity;
            ingredient.RecipeId = request.RecipeId;

            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();

            return new IngredientResponse(
                ingredient.Id,
                ingredient.Name,
                ingredient.Quantity,
                ingredient.RecipeId
            );
        }



        // delete ingredient by id
        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return false;
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
