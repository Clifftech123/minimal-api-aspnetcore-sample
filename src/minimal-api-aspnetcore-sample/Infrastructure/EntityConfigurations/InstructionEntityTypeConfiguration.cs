using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using minimal_api_aspnetcore_sample.Models;

namespace minimal_api_aspnetcore_sample.Infrastructure.EntityConfigurations
{
    public class InstructionEntityTypeConfiguration : IEntityTypeConfiguration<Instruction>
    {
        public void Configure(EntityTypeBuilder<Instruction> builder)
        {
            builder.ToTable(Instruction.TableName);

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(i => i.StepNumber)
                   .IsRequired();

            builder.HasOne(i => i.Recipe)
                   .WithMany(r => r.Instructions)
                   .HasForeignKey(i => i.RecipeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Instruction
                {
                    Id = 1,
                    Description =
                "Preheat oven to 350 degrees",
                    StepNumber = 1,
                    RecipeId = 1
                },
                new Instruction
                {
                    Id = 2,
                    Description = "Season chicken with salt and pepper",
                    StepNumber = 2,
                    RecipeId = 1
                },
                new Instruction { Id = 3, Description = "Heat olive oil in a large skillet over medium heat", StepNumber = 3, RecipeId = 1 },
                new Instruction { Id = 4, Description = "Cook chicken until golden brown", StepNumber = 4, RecipeId = 1 },
                new Instruction { Id = 5, Description = "Preheat oven to 350 degrees", StepNumber = 1, RecipeId = 2 },
                new Instruction { Id = 6, Description = "Season chicken with salt and pepper", StepNumber = 2, RecipeId = 2 }
                );
        }
    }
}
