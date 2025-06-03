using IservInternship.Domain.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IservInternship.Application.Infrastructure;

public class ApplicationContext : DbContext
{
    public virtual DbSet<ApplicationEntity> Applications => Set<ApplicationEntity>();
    public virtual DbSet<AnswerEntity> Answers => Set<AnswerEntity>();

    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var assembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
