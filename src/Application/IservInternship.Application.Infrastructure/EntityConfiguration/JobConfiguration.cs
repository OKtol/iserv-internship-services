using IservInternship.Domain.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IservInternship.Application.Infrastructure.EntityConfiguration;

public class JobConfiguration : IEntityTypeConfiguration<JobEntity>
{
    public void Configure(EntityTypeBuilder<JobEntity> builder)
    {
        builder.ToTable("jobs");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Title).HasColumnName("title");
        builder.Property(e => e.IsVisible).HasColumnName("is_visible");

        builder.HasKey(e => e.Id);
    }
}
