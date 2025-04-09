using Persistence.Entities;

namespace Bussines.Services
{
    public interface ICustomerService
    {
        void CreateCustomer(string name, string gender, int months);
        List<Customer> GetAll();
        Customer? GetId(string id);
    }

    public class CustomerService : ICustomerService
    {
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

        public void CreateCustomer(string name,string gender, int months)
        {
            var customer = new Customer
            {
                Name = name,
                Gender = gender switch
                {
                    "male" => true,
                    "female" => false,
                    _ => null
                },
                SubscriptionExpiresAt = DateTime.UtcNow.AddMonths(months)
            };
        }

        public List<Customer> GetAll()
        {
            return customers;
        }

        public Customer? GetId(string id)
        {
            return customers.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
