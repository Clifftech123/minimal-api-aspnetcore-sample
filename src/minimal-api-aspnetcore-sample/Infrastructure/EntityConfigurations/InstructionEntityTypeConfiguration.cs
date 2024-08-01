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
        }
    }
}
