using IservInternship.Domain.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IservInternship.Application.Infrastructure.EntityConfiguration;

public class ApplicationConfiguration : IEntityTypeConfiguration<ApplicationEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
    {
        builder.ToTable("applications");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.UserUid).HasColumnName("user_uid");
        builder.Property(e => e.JobId).HasColumnName("job_id");

        builder.Property(e => e.FirstName).HasColumnName("first_name");
        builder.Property(e => e.LastName).HasColumnName("last_name");
        builder.Property(e => e.Email).HasColumnName("email");
        builder.Property(e => e.PhoneNumber).HasColumnName("phone_number");
        builder.Property(e => e.AboutMe).HasColumnName("about_me");

        builder.Property(e => e.TestTask).HasColumnName("test_task");
        builder.Property(e => e.CorrectAnswer).HasColumnName("correct_answer");

        builder.Property(e => e.Solution).HasColumnName("solution");
        builder.Property(e => e.Answer).HasColumnName("answer");

        builder.Property(e => e.VerificationStatus).HasColumnName("verification_status");
        builder.Property(e => e.SolutionStatus).HasColumnName("solution_status");
        builder.Property(e => e.Status).HasColumnName("status");

        builder.HasKey(e => e.Id);
        builder
            .HasOne(e => e.Job)
            .WithMany(e => e.Applications)
            .HasForeignKey(e => e.JobId);
    }
}
