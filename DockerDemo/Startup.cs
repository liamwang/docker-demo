using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DockerDemo.Models;
using Microsoft.Extensions.Configuration;

namespace DockerDemo
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
            var dbhost = Configuration["DBHOST"];
            var connection = $"server={dbhost};database=dockerdemo;uid=postgres;pwd=123456";

            services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(
                // Configuration.GetConnectionString("DefaultConnection")));
                connection));

            services.AddSingleton(Configuration);
            services.AddTransient<IRepository, ProductRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
