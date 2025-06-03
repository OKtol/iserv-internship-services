using IservInternship.Application.Infrastructure;
using IservInternship.BFF.Web.Extensions;
using IservInternship.Commons.Extensions;
using System.Globalization;

namespace IservInternship.BFF.Web;

public class Program
{
    public static void Main(string[] args)
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
        }

        builder
            .ConfigureSettings()
            .ConfigurePostgresDatabase<ApplicationContext>()
            .ConfigureControllers()
            .ConfigureAuth()
            .ConfigureServices();

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi();
        }

        app.MapControllers();

        app.Run();
    }
}
