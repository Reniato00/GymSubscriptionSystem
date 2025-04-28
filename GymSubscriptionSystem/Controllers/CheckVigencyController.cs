using System.Threading.Tasks;
using Bussines.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    [Authorize]
    public class CheckVigencyController : Controller
    {
        private readonly ILogger<CheckVigencyController> _logger;
        private readonly ICustomerService customerService;

        public CheckVigencyController(ILogger<CheckVigencyController> logger, ICustomerService customerService)
        {
            _logger = logger;
            this.customerService = customerService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> Scan(string id)
        {
            var customer = await customerService.GetStatusAndName(id);

            if(customer.Item1 == "Active")
            {
                TempData["Success"] = $"Subscription is active. Name: {customer.Item2}";
            }
            else if(customer.Item1 == "ExpiringSoon")
            {
                TempData["Warning"] = $"Subscription is expiring soon. Name: {customer.Item2}";
            }
            else if(customer.Item1 == "Expired")
            {
                TempData["Error"] = $"Subscription has expired. Name: {customer.Item2}";
            }
            else
            {
                TempData["Error"] = "Something went wrong.";
            }
            

            return RedirectToAction("Index");
        }
    }
}