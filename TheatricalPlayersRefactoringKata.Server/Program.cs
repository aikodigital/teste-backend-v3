using DotNetEnv;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;

using TheatricalPlayersRefactoringKata.Server.Database;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories;
using TheatricalPlayersRefactoringKata.Server.Mappers;
using AutoMapper;

namespace TheatricalPlayersRefactoringKata.Server
{
    public class Program
    {
        private static readonly int PORT = 3000;

        public static void Main(string[] arguments)
        {
            // Load environment variables from .env file
            Env.Load(
                options: new LoadOptions(
                    clobberExistingVars: false
                )
            );

            WebApplicationBuilder builder = WebApplication.CreateBuilder(arguments);

            // Construct the database URL
            StringBuilder databaseUrl = new StringBuilder()
                .Append("server=")
                .Append(Environment.GetEnvironmentVariable("DATABASE_HOST"))
                .Append(";database=")
                .Append(Environment.GetEnvironmentVariable("DATABASE_NAME"))
                .Append(";user=")
                .Append(Environment.GetEnvironmentVariable("DATABASE_USERNAME"))
                .Append(";password=")
                .Append(Environment.GetEnvironmentVariable("DATABASE_PASSWORD"));

            // Configure the database context
            builder.Services.AddDbContext<DbContextTheatricalPlayers>(options =>
            {
                options.UseMySql(databaseUrl.ToString(), new MySqlServerVersion(new Version(8, 0, 21)));
            });

            // Configure AutoMapper
            builder.Services.AddSingleton(
                new MapperConfiguration(configure =>
                {
                    configure.AddProfile(new PlayMappingProfile());
                    configure.AddProfile(new PerformanceMappingProfile());
                }).CreateMapper());

            // Register repositories
            builder.Services.AddScoped<PlayRepository>();

            // Configure Kestrel to listen on port {PORT} with HTTPS
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Listen(IPAddress.Any, PORT, listenOptions =>
                {
                    listenOptions.UseHttps();
                });
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins($"https://localhost:{PORT}")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure the web application
            WebApplication app = builder.Build();

            // Automatically apply any pending migrations
            using (IServiceScope serviceScope = app.Services.CreateScope())
            {
                DbContextTheatricalPlayers context = serviceScope.ServiceProvider.GetRequiredService<DbContextTheatricalPlayers>();
                context.Database.Migrate();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors("AllowSpecificOrigin");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}