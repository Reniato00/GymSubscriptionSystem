using Persistence.Entities;

namespace GymSubscriptionSystem.Models
{
    public class ExpiredPeopleViewModel
    {
        public List<Customer> ExpiredPeople { get; set; }
        public int MonthSelect { get; set; }
        public string SelectedIds { get; set; }
    }
}