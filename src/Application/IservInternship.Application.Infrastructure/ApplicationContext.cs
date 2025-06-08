using IservInternship.Domain.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IservInternship.Application.Infrastructure;

public class ApplicationContext : DbContext
{
    public virtual DbSet<ApplicationEntity> Applications => Set<ApplicationEntity>();
    public virtual DbSet<JobEntity> Jobs => Set<JobEntity>();

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
