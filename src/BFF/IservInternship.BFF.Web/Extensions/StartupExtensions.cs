using Asp.Versioning;
using IservInternship.Application.Services;
using IservInternship.BFF.Web.Mappings;
using IservInternship.BFF.Web.Options;
using IservInternship.Commons.Configuration;
using IservInternship.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Security.Claims;
using System.Text.Json;

namespace IservInternship.BFF.Web.Extensions;

public static class StartupExtensions
{
    public static WebApplicationBuilder ConfigureSettings(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        builder.Services.AddOptions<KeycloackAuthOptions>()
            .Bind(configuration.GetSection(nameof(KeycloackAuthOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services.AddOptions<DatabaseOptions>()
                .Bind(builder.Configuration.GetSection(nameof(DatabaseOptions)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

        return builder;

        // grpc

        // if is development add dev cors options

        // rabbit

        // bus route

        // minio
    }

    // configure grpc

    public static WebApplicationBuilder ConfigureControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            // filters
        });

        return builder;
    }

    // configure masstransit

    public static WebApplicationBuilder ConfigureAuth(this WebApplicationBuilder builder)
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var keycloackAuthOptions = serviceProvider.GetRequiredService<IOptions<KeycloackAuthOptions>>().Value;
        var authorityUriWithRealm = new Uri(
            new Uri(keycloackAuthOptions.AuthorityUri),
            new PathString("/realms")
            .Add("/" + keycloackAuthOptions.Realm));

        builder.Services
            .AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
            })
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = authorityUriWithRealm.AbsoluteUri;
                options.Audience = keycloackAuthOptions.Audience;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // Извлекаем Principal из контекста
                        var principal = context.Principal;

                        // Проверяем, что Principal содержит объект ClaimsIdentity
                        if (principal?.Identity is ClaimsIdentity identity)
                        {
                            // Извлекаем значение realm_access из токена
                            var realmAccess = principal.Claims.FirstOrDefault(c => c.Type == "realm_access")?.Value;
                            if (!string.IsNullOrWhiteSpace(realmAccess))
                            {
                                try
                                {
                                    // Парсим JSON и извлекаем массив ролей
                                    using var document = JsonDocument.Parse(realmAccess);
                                    var roles = document.RootElement
                                        .GetProperty("roles")
                                        .EnumerateArray()
                                        .Select(token => token.GetString() ?? string.Empty);

                                    // Добавляем каждую роль как отдельный Claim с типом ClaimTypes.Role
                                    foreach (var role in roles)
                                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                                }
                                catch (JsonException ex)
                                {
                                    // Логируем ошибку парсинга JSON
                                    context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                                        .CreateLogger(nameof(JwtBearerEvents))
                                        .LogWarning(ex, "Error parsing realm_access JSON.");
                                }
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return builder;
    }

    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddAutoMapper(typeof(ApplicationProfile));

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IUserContextService, ProductionUserContextService>();
        builder.Services.AddTransient<ApplicationService>();
        builder.Services.AddTransient<JobService>();

        builder.Services
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        builder.Services.AddOpenApiDocument(doc =>
        {
            doc.AddSecurity("bearer", new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Bearer token authorization header",
            });

            doc.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        });
            
        return builder;
    }
}
