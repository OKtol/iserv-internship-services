using IservInternship.Domain.Account.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IservInternship.Account.Infrastructure;

public class AccountContext : DbContext
{
    public virtual DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();

    public virtual DbSet<AnswerEntity> Answers => Set<AnswerEntity>();

    public AccountContext()
    {

    }

    public AccountContext(DbContextOptions<AccountContext> options)
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
