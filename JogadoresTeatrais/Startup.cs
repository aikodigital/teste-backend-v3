using JogadoresTeatrais.IoC;
using JogaresTeatrais.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace JogadoresTeatrais
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

            var connectionString = Configuration.GetConnectionString("JogadoresTeatrais");
            services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));

            NativeInjector.RegisterServices(services);



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            var defaultCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
