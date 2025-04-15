using System.Threading.Tasks;
using Bussines.Extensions;
using Persistence.Entities;
using Persistence.Repositories;

namespace Bussines.Services
{
    public interface ICustomerService
    {
        Task CreateCustomer(string name, string gender, int months);
        Task<List<Customer>> GetAll();
        Task<List<Customer>> GetAll(string term, int skip, int take);
        Task<Customer?> GetId(string id);
        Task IncreaseSubscription(string customerId, int monthsToAdd);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IMiAppRepository db;
        public CustomerService(IMiAppRepository db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // <summary>
        // service to Create customers
        // </summary>
        public async Task CreateCustomer(string name,string gender, int months)
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
                LastUpdatedAt=DateTime.UtcNow
            };
            await db.CreateCustomerAsync(customer);
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
        }
    }
}
