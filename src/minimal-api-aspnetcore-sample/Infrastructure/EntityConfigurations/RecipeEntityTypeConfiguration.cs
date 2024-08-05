using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using minimal_api_aspnetcore_sample.Models;

namespace minimal_api_aspnetcore_sample.Infrastructure.EntityConfigurations
{
    public sealed class RecipeEntityTypeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable(Recipe.TableName);
            builder.HasKey(x => x.Id);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Description)
                .HasMaxLength(1000);

            builder.Property(r => r.Category)
                .HasMaxLength(50);

            builder.HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Instructions)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(

                new Recipe { Id = 1, Title = "Chicken Parmesan", Description = "A classic Italian dish made with breaded chicken, marinara sauce, and mozzarella cheese", Category = "Italian" },
              new Recipe { Id = 2, Title = "Chicken Alfredo", Description = "A creamy pasta dish made with fettuccine, chicken, and Alfredo sauce", Category = "Italian" },
              new Recipe { Id = 3, Title = "Chicken Marsala", Description = "A savory chicken dish made with Marsala wine, mushrooms, and garlic", Category = "Italian" },
              new Recipe { Id = 4, Title = "Chicken Piccata", Description = "A tangy chicken dish made with lemon, capers, and white wine", Category = "Italian" },
              new Recipe { Id = 5, Title = "Chicken Cacciatore", Description = "A hearty chicken stew made with tomatoes, onions, and bell peppers", Category = "Italian" },
              new Recipe { Id = 6, Title = "Chicken Florentine", Description = "A creamy chicken dish made with spinach, mushrooms, and Parmesan cheese", Category = "Italian" },
              new Recipe { Id = 7, Title = "Chicken Tetrazzini", Description = "A rich pasta dish made with chicken, mushrooms, and a creamy sauce", Category = "Italian" },
              new Recipe { Id = 8, Title = "Chicken Scallopini", Description = "A flavorful chicken dish made with lemon, capers, and white wine", Category = "Italian" },
              new Recipe { Id = 9, Title = "Chicken Saltimbocca", Description = "A delicious chicken dish made with prosciutto, sage, and white wine", Category = "Italian" },
              new Recipe { Id = 10, Title = "Chicken Francese", Description = "A light and tangy chicken dish made with lemon, butter, and white wine", Category = "Italian" }

              );
        }
    }
}
