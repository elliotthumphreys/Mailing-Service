using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using SimpleMailingService.Extensions;

namespace SimpleMailingService
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env) =>
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"Configuration/app-settings.json")
                .AddJsonFile($"Configuration/app-settings.{env.EnvironmentName}.json", true)
                .AddJsonFile($"Configuration/secrets.app-settings.json")
                .AddJsonFile($"Configuration/secrets.app-settings.{env.EnvironmentName}.json", true)
                .Build();

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddServices()
                .AddConfiguration(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Simple Mailing Service", Version = "v1"});
                c.IncludeXmlComments($"{PlatformServices.Default.Application.ApplicationBasePath}\\Simple-Mailing-Service.xml");
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Simple Mailing Service - Version 1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsEnvironment("local"))
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
