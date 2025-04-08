using Persistence.Entities;
using System.Reflection;

namespace Bussines.Services
{
    public interface ICustomerService
    {
        void CreateCustomer(string name, string gender, int months);
    }

    public class CustomerService : ICustomerService
    {
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
    }
}
