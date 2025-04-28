using Bussines.Services;
using Bussines.Extensions;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;
using Microsoft.AspNetCore.Authorization;

namespace GymSubscriptionSystem.Controllers
{
    [Authorize]
    public class DetailCustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private Customer? client;
        public DetailCustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            string? customerId = Request.Query["customer"];
            client = await customerService.GetId(customerId ?? "");
            var subscriptionExpiresAt = client?.SubscriptionExpiresAt;
            var vm = new DetailCustomerViewModel
            {
                Client = client,
                Status = subscriptionExpiresAt.GetStatus(),
            };
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> IncreaseSubscription(string customerId, int monthsToAdd)
        {
            await customerService.IncreaseSubscription(customerId, monthsToAdd);
            return RedirectToAction("Index", "DetailCustomer", new { customer = customerId });
        }
    }
}
