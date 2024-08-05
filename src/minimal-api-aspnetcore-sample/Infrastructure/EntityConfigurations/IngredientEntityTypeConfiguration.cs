using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using minimal_api_aspnetcore_sample.Models;

namespace minimal_api_aspnetcore_sample.Infrastructure.EntityConfigurations
{
    public sealed class IngredientEntityTypeConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable(Ingredient.TableName);

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(i => i.Quantity)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(i => i.Recipe)
                   .WithMany(r => r.Ingredients)
                   .HasForeignKey(i => i.RecipeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(

                    new Ingredient { Id = 1, Name = "Salt", Quantity = "1 tsp", RecipeId = 1 },
                    new Ingredient { Id = 2, Name = "Pepper", Quantity = "1 tsp", RecipeId = 1 },
                    new Ingredient { Id = 3, Name = "Olive Oil", Quantity = "1 tbsp", RecipeId = 1 },
                    new Ingredient { Id = 4, Name = "Chicken Breast", Quantity = "1 lb", RecipeId = 1 },
                    new Ingredient { Id = 5, Name = "Salt", Quantity = "1 tsp", RecipeId = 2 },
                    new Ingredient { Id = 6, Name = "Pepper", Quantity = "1 tsp", RecipeId = 2 },
                    new Ingredient { Id = 7, Name = "Olive Oil", Quantity = "1 tbsp", RecipeId = 2 },
                    new Ingredient { Id = 8, Name = "Chicken Breast", Quantity = "1 lb", RecipeId = 2 },
                    new Ingredient { Id = 9, Name = "Salt", Quantity = "1 tsp", RecipeId = 3 },
                    new Ingredient { Id = 10, Name = "Pepper", Quantity = "1 tsp", RecipeId = 3 },
                    new Ingredient { Id = 11, Name = "Olive Oil", Quantity = "1 tbsp", RecipeId = 3 },
                    new Ingredient { Id = 12, Name = "Chicken Breast", Quantity = "1 lb", RecipeId = 3 }


                );
        }

    }
}
