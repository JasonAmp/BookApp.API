using Microsoft.Extensions.DependencyInjection;

namespace BookAPP.Api.Utils
{
    public static class DomainServiceRegistration
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            //services.AddScoped<IConfigurationService, ConfigurationService>();
        }
    }
}