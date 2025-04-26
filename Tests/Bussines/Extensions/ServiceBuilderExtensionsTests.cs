using Bussines.Decorators;
using Bussines.Extensions;
using Bussines.Services;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Extensions;

namespace Tests.Bussines.Extensions
{
    public class ServiceBuilderExtensionsTests
    {
        [Fact]
        public void AddCustomService_RegistersServiceCorrectly()
        {
            var services = new ServiceCollection();
            services.AddServices();
            services.AddDecorators();
            services.AddRepositories();
            services.AddDbContext("Host=localhost;Port=5432;Database=GymSubscriptionSystem;Username=postgres;Password=1234");
            services.AddLogging();
            

            var serviceProvider = services.BuildServiceProvider();
            var customerService = serviceProvider.GetService<ICustomerService>();
            var loggerDecorator = serviceProvider.GetService<ILoggerDecorator<ICustomerService>>();
            Assert.NotNull(customerService);
            Assert.NotNull(loggerDecorator);
            Assert.IsType<LoggerDecorator<ICustomerService>>(loggerDecorator);
            Assert.IsType<CustomerService>(customerService);
        }
    }
}
