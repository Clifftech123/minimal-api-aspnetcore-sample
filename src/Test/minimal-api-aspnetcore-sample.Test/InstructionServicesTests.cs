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
    public class InstructionServicesTests
    {
        private readonly Mock<ApplicationContext> _mockContext;
        private readonly Mock<ILogger<InstructionServices>> _mockLogger;
        private readonly InstructionServices _service;

        public InstructionServicesTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .Options;
            var context = new ApplicationContext(options);

            _mockContext = new Mock<ApplicationContext>(options);
            _mockLogger = new Mock<ILogger<InstructionServices>>();
            _service = new InstructionServices(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateInstruction_ShouldReturnInstructionResponse()
        {
            // Arrange
            var request = new CreateInstructionRequest("Test", 1, 1);
            _mockContext.Setup(c => c.Instructions.Add(It.IsAny<Instruction>())).Callback<Instruction>(i => i.Id = 1);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.CreateInstruction(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Description);
            Assert.Equal(1, result.StepNumber);
            Assert.Equal(1, result.RecipeId);
        }

        [Fact]
        public async Task GetInstructionById_ShouldReturnInstructionResponse()
        {
            // Arrange
            var instruction = new Instruction { Id = 1, Description = "Test", StepNumber = 1, RecipeId = 1 };
            _mockContext.Setup(c => c.Instructions.FindAsync(1)).ReturnsAsync(instruction);

            // Act
            var result = await _service.GetInstructionById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Description);
            Assert.Equal(1, result.StepNumber);
            Assert.Equal(1, result.RecipeId);
        }

        [Fact]
        public async Task GetInstructionById_ShouldThrowException_WhenInstructionNotFound()
        {
            // Arrange
            _mockContext.Setup(c => c.Instructions.FindAsync(1)).ReturnsAsync((Instruction)null);

            // Act & Assert
            await Assert.ThrowsAsync<InstructionDoesNotExistException>(() => _service.GetInstructionById(1));
        }

        [Fact]
        public async Task UpdateInstruction_ShouldReturnUpdatedInstructionResponse()
        {
            // Arrange
            var request = new CreateInstructionRequest("Updated", 2, 1);
            var instruction = new Instruction { Id = 1, Description = "Test", StepNumber = 1, RecipeId = 1 };
            _mockContext.Setup(c => c.Instructions.FindAsync(1)).ReturnsAsync(instruction);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.UpdateInstruction(1, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Updated", result.Description);
            Assert.Equal(2, result.StepNumber);
            Assert.Equal(1, result.RecipeId);
        }

        [Fact]
        public async Task UpdateInstruction_ShouldThrowException_WhenInstructionNotFound()
        {
            // Arrange
            var request = new CreateInstructionRequest("Updated", 2, 1);
            _mockContext.Setup(c => c.Instructions.FindAsync(1)).ReturnsAsync((Instruction)null);

            // Act & Assert
            await Assert.ThrowsAsync<InstructionDoesNotExistException>(() => _service.UpdateInstruction(1, request));
        }

        [Fact]
        public async Task DeleteInstruction_ShouldReturnTrue()
        {
            // Arrange
            var instruction = new Instruction { Id = 1, Description = "Test", StepNumber = 1, RecipeId = 1 };
            _mockContext.Setup(c => c.Instructions.FindAsync(1)).ReturnsAsync(instruction);
            _mockContext.Setup(c => c.Instructions.Remove(instruction));
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.DeleteInstruction(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteInstruction_ShouldThrowException_WhenInstructionNotFound()
        {
            // Arrange
            _mockContext.Setup(c => c.Instructions.FindAsync(1)).ReturnsAsync((Instruction)null);

            // Act & Assert
            await Assert.ThrowsAsync<InstructionDoesNotExistException>(() => _service.DeleteInstruction(1));
        }
    }
}
