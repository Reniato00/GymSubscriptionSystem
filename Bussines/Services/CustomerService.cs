using Bussines.Decorators;
using Bussines.Extensions;
using Persistence.Entities;
using Persistence.Repositories;

namespace Bussines.Services
{
    public interface ICustomerService
    {
        Task CreateCustomer(string name, string gender, int months, string nameUser);
        Task<List<Customer>> GetAll();
        Task<List<Customer>> GetAll(string term, int skip, int take);
        Task<Customer?> GetId(string id);
        Task IncreaseSubscription(string customerId, int monthsToAdd);
        Task<(string?,string?)> GetStatusAndName(string id);
        Task<int> GetCostumersCount(string term);
        Task<List<Customer>> GetExpiredCustomer(int months);
        Task DeleteCustomer(string id);
        Task DeleteAllCustomers(string ids);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IMiAppRepository db;
        private readonly ILoggerDecorator<CustomerService> logger;
        public CustomerService(IMiAppRepository db, ILoggerDecorator<CustomerService> logger)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // <summary>
        // service to Create customers
        // </summary>
        public async Task CreateCustomer(string name,string gender, int months, string nameUser)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Gender = gender switch
                {
                    "male" => true,
                    "female" => false,
                    _ => null
                },
                SubscriptionExpiresAt = DateTime.UtcNow.AddMonths(months),
                CreatedAt=DateTime.UtcNow,
                LastUpdatedAt=DateTime.UtcNow,
                NameUser = nameUser
            };
            await db.CreateCustomerAsync(customer);
            logger.LogInformation("Admin", customer.Id, "created");
        }

        public async Task<List<Customer>> GetAll()
        {
            return  await db.GetAllCustomersAsync();
        }

        public async Task<List<Customer>> GetAll(string term, int skip, int take)
        {
            return await db.GetCustomersAsync(term,skip, take);
        }

        public async Task<Customer?> GetId(string id)
        {
            return await db.GetId(id);
        }

        public async Task IncreaseSubscription(string customerId, int monthsToAdd)
        {
            var client = await GetId(customerId);
            var subscriptionExpiresAt = client?.SubscriptionExpiresAt;
            var status = subscriptionExpiresAt.GetStatus();

            if (status == "Active" || status == "ExpiringSoon")
            {
                client!.SubscriptionExpiresAt = DateTime.SpecifyKind(client.SubscriptionExpiresAt.AddMonths(monthsToAdd), DateTimeKind.Utc);
                await db.UpdateSubscriptionCustomer(client);
            }
            else
            {
                client!.SubscriptionExpiresAt = DateTime.UtcNow.AddMonths(monthsToAdd);
                await db.UpdateSubscriptionCustomer(client);
            }

            logger.LogInformation("Admin", client.Id, "increased subscription");
        }

        public async Task<(string?,string?)> GetStatusAndName(string id)
        {
            var customer = await db.GetExpiredTimeAndName(id);
            return (customer?.Expired.GetStatus(), customer?.Name);
        }

        public async Task<int> GetCostumersCount(string term)
        {
            return await db.GetCostumersCountAsync(term);
        }

        public async Task<List<Customer>> GetExpiredCustomer(int months)
        {
            var customers = await db.GetExpiredCustomersAsync(months * -1);
            return customers;
        }

        public async Task DeleteCustomer(string id)
        {
            await db.DeleteCustomer(id);
            logger.LogInformation("Admin", id, "deleted");
        }

        public async Task DeleteAllCustomers(string ids)
        {
            await db.DeleteAllCustomers(ids);
            logger.LogInformation("Admin", ids, "deleted all");
        }
    }
}
