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
                .WithOne(r => r.Recipe)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
