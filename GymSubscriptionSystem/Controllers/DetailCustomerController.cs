using Bussines.Services;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace GymSubscriptionSystem.Controllers
{
    public class DetailCustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private Customer? client;
        public DetailCustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult Index()
        {
            string? customerId = Request.Query["customer"];
            client = customerService.GetId(customerId ?? "");
            var vm = new DetailCustomerViewModel
            {
                Client = client,
                Status = GetStatus(client?.SubscriptionExpiresAt)
            };
            return View(vm);
        }

        private static string GetStatus(DateTime? expired)
        {
            if (expired == null || DateTime.Now > expired.Value )
            {
                return "Expired";
            }

            if(DateTime.Now > expired.Value.AddDays(-5) && DateTime.Now < expired.Value)
            {
                return "ExpiringSoon";
            }

            return "Active";
        }

        [HttpPost]
        public IActionResult IncreaseSubscription(string customerId, int monthsToAdd)
        {
            client = customerService.GetId(customerId);
            var status = GetStatus(client!.SubscriptionExpiresAt);
            if(status == "Active" || status == "ExpiringSoon")
            {
                client.SubscriptionExpiresAt = client.SubscriptionExpiresAt.AddMonths(monthsToAdd);
            }
            else
            {
                client.SubscriptionExpiresAt = DateTime.Now.AddMonths(monthsToAdd);
            }

            return RedirectToAction("Index", "DetailCustomer", new { customer = customerId });
        }
    }
}
