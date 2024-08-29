using JogaresTeatrais.Data.Context;
using Microsoft.EntityFrameworkCore;

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
            //services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("JogadoresTeatrais")).EnableSensitiveDataLogging());
            var connectionString = Configuration.GetConnectionString("JogadoresTeatrais");
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(connectionString));


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
                endpoints.MapRazorPages();
            });
        }
    }
}
