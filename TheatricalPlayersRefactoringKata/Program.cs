using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5001);
            });

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Theatrical Players API", Version = "v1" });
            });

            builder.Services.AddScoped<TragedyCalculator>();
            builder.Services.AddScoped<ComedyCalculator>();
            builder.Services.AddScoped<HistoricalCalculator>();

            builder.Services.AddScoped<IEnumerable<IPlayTypeCalculator>>(sp => new IPlayTypeCalculator[]
            {
                sp.GetRequiredService<TragedyCalculator>(),
                sp.GetRequiredService<ComedyCalculator>()
            });

            builder.Services.AddScoped<IStatementGenerator, StatementGenerator>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Theatrical Players API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}