using IservInternship.Domain.Account.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IservInternship.Account.Infrastructure.EntityConfiguration;

public class ProfileConfiguration : IEntityTypeConfiguration<ProfileEntity>
{
    public void Configure(EntityTypeBuilder<ProfileEntity> builder)
    {
        builder.ToTable("profiles");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.UserUid).HasColumnName("user_uid");

        builder.HasKey(x => x.Id);
    }
}
