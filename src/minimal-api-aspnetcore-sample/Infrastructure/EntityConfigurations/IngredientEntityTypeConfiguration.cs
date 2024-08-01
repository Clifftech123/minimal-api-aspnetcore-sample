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
        }
    }
}
