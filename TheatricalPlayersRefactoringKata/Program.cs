using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;
using TheatricalPlayersRefactoringKata.Infrastructure.Converters;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Infrastructure;

namespace TheatricalPlayersRefactoringKata
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5001);
            });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new GenreConverter());
                    options.JsonSerializerOptions.Converters.Add(new PlayConverter());
                });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API dos Jogadores Teatrais", Version = "v1" });
            });

            builder.Services.AddTransient<TragedyCalculator>();
            builder.Services.AddTransient<ComedyCalculator>();
            builder.Services.AddTransient<IPerformanceCalculator, TragedyCalculator>();
            builder.Services.AddTransient<IPerformanceCalculator, ComedyCalculator>();

            builder.Services.AddTransient<HistoricalCalculator>();

            builder.Services.AddTransient<IPerformanceCalculatorFactory, PerformanceCalculatorFactory>();

            builder.Services.AddTransient<TextStatementGenerator>();
            builder.Services.AddTransient<XmlStatementGenerator>();
            builder.Services.AddScoped<IStatementGenerator, TextStatementGenerator>();
            builder.Services.AddScoped<IStatementGenerator, XmlStatementGenerator>();

            builder.Services.AddTransient<PerformanceFactory>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API dos Jogadores Teatrais V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}