namespace Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Persistence.Entities;
    using GymSubscriptionSystem.Persistence;

    public interface IMiAppRepository
    {
        Task<List<Customer>> GetCustomersAsync(string term,int skip, int take);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetId(string id);
        Task<int> CreateCustomerAsync(Customer customer);
        Task<List<Plans>> GetPlansAsync();
        Task<List<Instructor>> GetInstructorsAsync();
        Task<int> UpdateSubscriptionCustomer(Customer customer);
        Task<CustomerDto?> GetExpiredTimeAndName(string id);
        Task<int> GetCostumersCountAsync(string term);
        Task<List<Customer>> GetExpiredCustomersAsync(int months);
        Task DeleteCustomer(string id);
        Task DeleteAllCustomers(string ids);
        Task<Instructor?> GetInstructor(string username, string password);
        Task<Instructor?> GetInstructor(string name);
    }

    public class MiAppRepository : IMiAppRepository
    {
        private readonly Context context;

        public MiAppRepository(Context context)
        {
            this.context = context;
        }

        public async Task<List<Customer>> GetCustomersAsync(string term,int skip, int take)
        {
            return await context.Customers
            .Where(c => c.Name.ToLower().Contains(term))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<int> GetCostumersCountAsync(string term)
        {
            return await context.Customers
            .Where(c=> c.Name.ToLower().Contains(term))
            .CountAsync();
        }

        public async Task<Customer?> GetId(string id)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            await context.Customers.AddAsync(customer);
            return await context.SaveChangesAsync();
        }

        public async Task<List<Plans>> GetPlansAsync()
        {
            return await context.Plans.ToListAsync();
        }

        public async Task<List<Instructor>> GetInstructorsAsync()
        {
            return await context.Instructors.ToListAsync();
        }

        public async Task<int> UpdateSubscriptionCustomer(Customer customer)
        {
            context.Customers.Attach(customer);
            context.Entry(customer).Property(c=> c.SubscriptionExpiresAt).IsModified = true;
            return await context.SaveChangesAsync();
        }

        public async Task<CustomerDto?> GetExpiredTimeAndName(string id)
        {
            return await context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDto { Expired = c.SubscriptionExpiresAt, Name = c.Name })
                .FirstOrDefaultAsync();
        }

        public async Task<List<Customer>> GetExpiredCustomersAsync(int months)
        {
            return await context.Customers
                .Where(c => c.SubscriptionExpiresAt < DateTime.UtcNow.AddMonths(months))
                .ToListAsync();
        }

        public async Task DeleteCustomer(string id)
        {
            var sql = $"DELETE FROM customers WHERE id = '{id}'";
            await context.Database.ExecuteSqlRawAsync(sql);    
        }

        public async Task DeleteAllCustomers(string ids)
        {
            var idList = ids.Split(',').Select(id => $"'{id}'");
            var formattedIds = string.Join(",", idList);

            var sql = $"DELETE FROM customers WHERE id IN ({formattedIds})";
            await context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task<Instructor?> GetInstructor(string username, string password)
        {
            return await context.Instructors
                .FirstOrDefaultAsync(i => i.Name == username && i.Password == password);
        }

        public async Task<Instructor?> GetInstructor(string name)
        {
            return await context.Instructors
                .FirstOrDefaultAsync(i => i.Name == name);
        }
    }
}