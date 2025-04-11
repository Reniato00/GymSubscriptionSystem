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
        Task<List<Customer>> GetCustomersAsync(int skip, int take);
        Task<int> CreateCustomerAsync(Customer customer);
        Task<List<Plans>> GetPlansAsync();
        Task<List<Instructor>> GetInstructorsAsync();
    }

    public class MiAppRepository : IMiAppRepository
    {
        private readonly Context context;

        public MiAppRepository(Context context)
        {
            this.context = context;
        }

        public async Task<List<Customer>> GetCustomersAsync(int skip, int take)
        {
            return await context.Customers.Skip(skip).Take(take).ToListAsync();
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
    }
}