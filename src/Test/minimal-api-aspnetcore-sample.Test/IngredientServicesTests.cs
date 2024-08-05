using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using minimal_api_aspnetcore_sample.Infrastructure.Context;
using minimal_api_aspnetcore_sample.Infrastructure.Exceptions;
using minimal_api_aspnetcore_sample.Models;
using minimal_api_aspnetcore_sample.Services;
using Moq;

namespace minimal_api_aspnetcore_sample.Test.Services
{
    public class IngredientServicesTests
    {
        private readonly Mock<ApplicationContext> _mockContext;
        private readonly Mock<ILogger<IngredientServices>> _mockLogger;
        private readonly IngredientServices _service;

        public IngredientServicesTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .Options;

            _mockContext = new Mock<ApplicationContext>(options);
            _mockLogger = new Mock<ILogger<IngredientServices>>();
            _service = new IngredientServices(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateIngredientAsync_ShouldReturnIngredientResponse()
        {
            // Arrange
            var request = new CreateIngredientRequest("Sugar", "100 cups", 1);
            _mockContext.Setup(c => c.Ingredients.Add(It.IsAny<Ingredient>())).Callback<Ingredient>(i => i.Id = 1);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.CreateIngredientAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Sugar", result.Name);
            Assert.Equal("100 cups", result.Quantity);
            Assert.Equal(1, result.RecipeId);
        }

        [Fact]
        public async Task GetIngredientsAsync_ShouldReturnAllIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Sugar", Quantity = "200 cups", RecipeId = 1 },
                new Ingredient { Id = 2, Name = "Salt", Quantity = "200", RecipeId = 1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Ingredient>>();
            mockSet.As<IQueryable<Ingredient>>().Setup(m => m.Provider).Returns(ingredients.Provider);
            mockSet.As<IQueryable<Ingredient>>().Setup(m => m.Expression).Returns(ingredients.Expression);
            mockSet.As<IQueryable<Ingredient>>().Setup(m => m.ElementType).Returns(ingredients.ElementType);
            mockSet.As<IQueryable<Ingredient>>().Setup(m => m.GetEnumerator()).Returns(ingredients.GetEnumerator());
            mockSet.As<IAsyncEnumerable<Ingredient>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<Ingredient>(ingredients.GetEnumerator()));

            _mockContext.Setup(c => c.Ingredients).Returns(mockSet.Object);

            // Act
            var result = await _service.GetIngredientsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }


         // get ingredient by id   test case
        [Fact]
        public async Task GetIngredientByIdAsync_ShouldReturnIngredientResponse()
        {
            // Arrange
            var ingredient = new Ingredient { Id = 1, Name = "Sugar", Quantity = "100 cups", RecipeId = 1 };
            _mockContext.Setup(c => c.Ingredients.FindAsync(1)).ReturnsAsync(ingredient);

            // Act
            var result = await _service.GetIngredientByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Sugar", result.Name);
            Assert.Equal("100 cups", result.Quantity);
            Assert.Equal(1, result.RecipeId);
        }

        [Fact]


         // get ingredient by id   test case
        public async Task GetIngredientByIdAsync_ShouldThrowException_WhenIngredientNotFound()
        {
            // Arrange
            _mockContext.Setup(c => c.Ingredients.FindAsync(1)).ReturnsAsync((Ingredient)null);

            // Act & Assert
            await Assert.ThrowsAsync<IngredientDoesNotExistException>(() => _service.GetIngredientByIdAsync(1));
        }

        [Fact]

        // update ingredient by id test case
        public async Task UpdateIngredientAsync_ShouldReturnUpdatedIngredientResponse()
        {
            // Arrange
            var request = new CreateIngredientRequest("Updated Sugar", "300 cups", 1);
            var ingredient = new Ingredient { Id = 1, Name = "Sugar", Quantity = "300 cups", RecipeId = 1 };
            _mockContext.Setup(c => c.Ingredients.FindAsync(1)).ReturnsAsync(ingredient);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.UpdateIngredientAsync(1, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Updated Sugar", result.Name);
            Assert.Equal("300 cups", result.Quantity);
            Assert.Equal(1, result.RecipeId);
        }


        // update ingredient by id test case

        [Fact]
        public async Task UpdateIngredientAsync_ShouldThrowException_WhenIngredientNotFound()
        {
            // Arrange
            var request = new CreateIngredientRequest("Updated Sugar", "200 cups", 1);
            _mockContext.Setup(c => c.Ingredients.FindAsync(1)).ReturnsAsync((Ingredient)null);

            // Act & Assert
            await Assert.ThrowsAsync<IngredientDoesNotExistException>(() => _service.UpdateIngredientAsync(1, request));
        }


        // delete ingredient by id test case
        [Fact]
        public async Task DeleteIngredientAsync_ShouldReturnTrue()
        {
            // Arrange
            var ingredient = new Ingredient { Id = 1, Name = "Sugar", Quantity = "200 cups", RecipeId = 1 };
            _mockContext.Setup(c => c.Ingredients.FindAsync(1)).ReturnsAsync(ingredient);
            _mockContext.Setup(c => c.Ingredients.Remove(ingredient));
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.DeleteIngredientAsync(1);

            // Assert
            Assert.True(result);
        }

        // delete ingredient by id test case
        [Fact]
        public async Task DeleteIngredientAsync_ShouldReturnFalse_WhenIngredientNotFound()
        {
            // Arrange
            _mockContext.Setup(c => c.Ingredients.FindAsync(1)).ReturnsAsync((Ingredient)null);

            // Act
            var result = await _service.DeleteIngredientAsync(1);

            // Assert
            Assert.False(result);
        }
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }

        public T Current => _inner.Current;
    }
}
