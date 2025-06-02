using IservInternship.Commons.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Reflection;

namespace IservInternship.Commons.Extensions;

public static class DatabaseExtensions
{
    public static WebApplicationBuilder ConfigurePostgresDatabase<T>(this WebApplicationBuilder builder)
        where T : DbContext
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

        // Используем стек вызовов, чтобы получить имя вызывающей сборки
        var migrationsAssembly = GetCallingAssemblyName();
        builder.Services.AddDbContext<T>(options =>
        {
            options.UseNpgsql(databaseOptions.ConnectionString, b => b.MigrationsAssembly(migrationsAssembly));
            if (builder.Environment.IsDevelopment())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        return builder;
    }

    // Метод для получения имени вызывающей сборки
    private static string GetCallingAssemblyName()
    {
        var stackTrace = new StackTrace();
        for (int i = 0; i < stackTrace.FrameCount; i++)
        {
            var method = stackTrace.GetFrame(i)?.GetMethod();
            var assembly = method?.DeclaringType?.Assembly;
            if (assembly != null && assembly != Assembly.GetExecutingAssembly())
            {
                return assembly.GetName().Name!;
            }
        }
        return Assembly.GetExecutingAssembly().GetName().Name!; // fallback на текущую сборку, если не найдена другая
    }
}
