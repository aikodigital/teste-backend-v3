using Microsoft.OpenApi.Models;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;
using TheatricalPlayersRefactoringKata.Infrastructure.RabbitMq;

namespace TheatricalPlayersRefactoringKata.API.Extensions;

/// <summary>
/// Provides extension methods for configuring various aspects of the application, such as API versioning, Swagger, and environment settings.
/// </summary>
public static class AppConfigurationExtensions
{
    private const string DefaultConnection = "DefaultConnection";
    private const string RabbitMq = "RabbitMq";

    /// <summary>
    /// Configures API versioning for the application.
    /// </summary>
    /// <param name="services">The service collection to add the API versioning services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        }).AddMvc();

        return services;
    }

    /// <summary>
    /// Configures Swagger for the application, including dynamically generating documentation for all API versions.
    /// </summary>
    /// <param name="service">The service collection to add the Swagger services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureSwagger(this IServiceCollection service)
    {

        service.AddSwaggerGen(c =>
        {
            // Retrieve all API versions using reflection
            var apiVersionDescriptionProvider = service
                .BuildServiceProvider()
                .GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                c.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"TheatricalPlayers API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "API from Aiko Digital - Vinícius Ribeiro",
                    Contact = new OpenApiContact() { Name = "Aiko Digital - Vinícius Ribeiro", Email = "aikodigital.teste.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org.licenses/MIT") },
                });
            }

            // You can add JWT authentication if necessary
            //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //{
            //    Description = "Enter the JWT token like this: Bearer {your token}\n\n" +
            //                  "Example: Bearer eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1",
            //    Name = "Authorization",
            //    Scheme = "Bearer",
            //    BearerFormat = "JWT",
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.ApiKey
            //});

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
        });

        return service;
    }

    /// <summary>
    /// Configures environment settings for the application.
    /// </summary>
    /// <param name="builder">The web application builder to configure.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder ConfigureEnvironmentSettings(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (builder.Environment.IsDevelopment())
            builder.Configuration.AddUserSecrets<Program>();

        return builder;
    }

    /// <summary>
    /// Configures the application to use SQL Server with the specified connection string.
    /// </summary>
    /// <param name="builder">The web application builder to configure.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder ConfigureSqlServer(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(DefaultConnection);
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return builder;
    }

    /// <summary>
    /// Configures RabbitMQ settings and registers the RabbitMqClient as a singleton service.
    /// </summary>
    /// <param name="builder">The web application builder to configure.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder ConfigureRabbitMq(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(RabbitMq));

        builder.Services.AddSingleton<RabbitMqClient>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
            return new RabbitMqClient(settings);
        });

        return builder;
    }
}