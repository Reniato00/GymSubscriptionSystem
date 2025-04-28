using Bussines.Services;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    [Authorize]
    public class ExpiredPeopleController : Controller
    {
        private readonly ICustomerService customerService;
        public ExpiredPeopleController(ICustomerService customerService)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));    
        }

        public async Task<IActionResult> Index(int monthSelect = 1)
        {
            var allCustomers = await customerService.GetExpiredCustomer(monthSelect);
            var vm = new ExpiredPeopleViewModel
            {
                ExpiredPeople = allCustomers,
                MonthSelect = monthSelect,
                SelectedIds = string.Join(",",allCustomers.Select(c => c.Id).ToList())
            };

            return View(vm);
        }

        [HttpPost]
        public async  Task<IActionResult> DeleteCustomer(string customerId)
        {
            customerService.DeleteCustomer(customerId);
            return RedirectToAction("Index", "ExpiredPeople", new { monthSelect = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllCustomers(string selectedIds)
        {
            customerService.DeleteAllCustomers(selectedIds);
            return RedirectToAction("Index", "ExpiredPeople", new { monthSelect = 1 });
        }
    }
}