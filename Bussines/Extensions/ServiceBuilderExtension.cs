using Bussines.Decorators;
using Bussines.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bussines.Extensions
{
    public static class ServiceBuilderExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            return services;
        }

        public static IServiceCollection AddDecorators(this IServiceCollection services)
        {
            services.AddScoped(typeof(ILoggerDecorator<>), typeof(LoggerDecorator<>));
            return services;
        }
    }
}
