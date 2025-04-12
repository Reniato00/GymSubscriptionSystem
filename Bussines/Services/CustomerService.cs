using System.Threading.Tasks;
using Persistence.Entities;
using Persistence.Repositories;

namespace Bussines.Services
{
    public interface ICustomerService
    {
        Task CreateCustomer(string name, string gender, int months);
        Task<List<Customer>> GetAll();
        Task<Customer?> GetId(string id);
        Task IncreaseSubscription(string status, int monthsToAdd, Customer client);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IMiAppRepository db;
        public CustomerService(IMiAppRepository db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        private List<Customer> customers = new List<Customer>()
            {
                new()
                {
                    Id = "A540",
                    Name = "renato Abimael Ramirez Lopez",
                    Gender = true,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(1),
                },
                new()
                {
                    Id = "AASDJ7",
                    Name = "Sam itzel quiroz loera",
                    Gender = false,
                    SubscriptionExpiresAt = DateTime.Now.AddDays(-2),
                },
                new()
                {
                    Id = "AJ450",
                    Name = "Lupe",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddDays(2),
                },
                new()
                {
                    Id = "48383DF",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "234REF",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "0495MVNF",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "KSDFJUEWR87",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "SDFWERCXV23",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "lmkdjsh9084",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "JNDCHUFR76",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "MZNXGSFYR76",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "938473628231D",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "VGCFEDWS476",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "IJUHYGTF873",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "QWERYUT983",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                },
                new()
                {
                    Id = "873DHCJF746",
                    Name = "Marie",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                }
                ,
                new()
                {
                    Id = "934JFURY7485UJ",
                    Name = "Luna",
                    Gender = null,
                    SubscriptionExpiresAt = DateTime.Now.AddMonths(12),
                }
            };

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

        public async Task<Customer?> GetId(string id)
        {
            return await db.GetId(id);
        }

        public async Task IncreaseSubscription(string status, int monthsToAdd, Customer client)
        {
            if (status == "Active" || status == "ExpiringSoon")
            {
                client.SubscriptionExpiresAt = DateTime.SpecifyKind(client.SubscriptionExpiresAt.AddMonths(monthsToAdd), DateTimeKind.Utc);
                await db.UpdateSubscriptionCustomer(client);
            }
            else
            {
                client.SubscriptionExpiresAt = DateTime.UtcNow.AddMonths(monthsToAdd);
                await db.UpdateSubscriptionCustomer(client);
            }
        }
    }
}
