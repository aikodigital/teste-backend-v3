using Microsoft.OpenApi.Models;
using TheatricalPlayersRefactoringKata.Infrastructure.Persistence;
using TheatricalPlayersRefactoringKata.Infrastructure.Persistence.Repositories;
using TheatricalPlayersRefactoringKata.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Theatrical Plays API",
                    Version = "v1",
                    Description = "API for Plays, Performances and Invoices",
                })
            );

            //Repository services.
            builder.Services.AddDbContext<DBContext>();
            builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IPlayRepository, PlayRepository>();
            builder.Services.AddScoped<IPerformanceRepository, PerformanceRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Theatrical API v1")
                );
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}