using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace GymSubscriptionSystem.Controllers
{
    public class ExpiredPeopleController : Controller
    {
        public IActionResult Index()
        {
            var expiredPeople = new List<Customer>
            {
                new Customer { Name = "John Doe", SubscriptionExpiresAt = DateTime.Now.AddDays(-1) },
                new Customer { Name = "Jane Smith", SubscriptionExpiresAt = DateTime.Now.AddDays(-2) },
                new Customer { Name = "Alice Johnson", SubscriptionExpiresAt = DateTime.Now.AddDays(-3) }
            };

            var vm = new ExpiredPeopleViewModel
            {
                ExpiredPeople = expiredPeople
            };
            return View(vm);
        }
    }
}