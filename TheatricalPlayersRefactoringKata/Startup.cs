using System;
using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Services;
using TheatricalPlayersRefactoringKata.Repositories;
using TheatricalPlayersRefactoringKata.Repositories.Interface;
using TheatricalPlayersRefactoringKata.Services.Interface;

namespace TheatricalPlayersRefactoringKata
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Configure DbContext with SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TheatricalPlayersRefactoringKataDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"));

            // Register AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Register Repositories and Services
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IStatementPrinterService, StatementPrinterService>();

            // Configure Swagger for API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Theatrical Players API",
                    Version = "v1",
                    Description = "API for managing theatrical invoices and performances",
                    Contact = new OpenApiContact
                    {
                        Name = "Developer Name",
                        Email = "developer@example.com",
                    }
                });

                // Include XML comments for better documentation
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Theatrical Players API V1");
                    c.RoutePrefix = string.Empty; // Isso faz o Swagger ser acessível pela raiz
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
