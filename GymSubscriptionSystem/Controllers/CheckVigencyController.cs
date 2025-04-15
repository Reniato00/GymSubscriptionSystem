using Bussines.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{


    public class CheckVigencyController : Controller
    {
        private readonly ILogger<CheckVigencyController> _logger;
        private readonly ICustomerService customerService;

        public CheckVigencyController(ILogger<CheckVigencyController> logger, ICustomerService customerService)
        {
            _logger = logger;
            this.customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            TempData["Success"] = "Subscription updated successfully.";
            TempData["Error"] = "Something went wrong updating the subscription.";

            

            return View();
        }
    }
}