using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMailingService.Models;
using SimpleMailingService.Services;

namespace SimpleMailingService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ISendService, SendService>();

            return services;
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ClientOptions>(configuration.GetSection(ClientOptions.ConfigurationName));

            return services;
        }
    }
}
