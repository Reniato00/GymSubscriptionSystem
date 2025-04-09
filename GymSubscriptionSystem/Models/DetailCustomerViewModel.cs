using Persistence.Entities;

namespace GymSubscriptionSystem.Models
{
    public class DetailCustomerViewModel
    {
        public Customer? Client { get; set; }
        public required string Status { get; set; }

        public int MonthsToAdd { get; set; }
    }
}
