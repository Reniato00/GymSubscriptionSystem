using Persistence.Entities;

namespace GymSubscriptionSystem.Models
{
    public class IndexViewModel
    {
        public List<Customer> Customers { get; set; } = new();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public string SearchTerm { get; set; }
    }
}
