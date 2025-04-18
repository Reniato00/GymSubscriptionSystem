using Bussines.Services;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    public class ExpiredPeopleController : Controller
    {
        private readonly ICustomerService customerService;
        public ExpiredPeopleController(ICustomerService customerService)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));    
        }

        public async Task<IActionResult> Index(int monthSelect = 1)
        {
            var vm = new ExpiredPeopleViewModel
            {
                ExpiredPeople = await customerService.GetExpiredCustomer(monthSelect),
                MonthSelect = monthSelect 
            };
            return View(vm);
        }
    }
}