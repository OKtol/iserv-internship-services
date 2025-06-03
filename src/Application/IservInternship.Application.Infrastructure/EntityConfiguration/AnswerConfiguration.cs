using IservInternship.Domain.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IservInternship.Application.Infrastructure.EntityConfiguration;

public class AnswerConfiguration : IEntityTypeConfiguration<AnswerEntity>
{
    public void Configure(EntityTypeBuilder<AnswerEntity> builder)
    {
        builder.ToTable("answers");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Text).HasColumnName("text");
        builder.Property(e => e.Status).HasColumnName("status");
        builder.Property(e => e.ApplicationId).HasColumnName("application_id");

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Application)
            .WithOne(e => e.Answer)
            .HasForeignKey<AnswerEntity>(e => e.ApplicationId);
    }
}
