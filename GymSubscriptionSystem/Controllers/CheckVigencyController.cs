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

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Scan(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                TempData["Success"] = "Subscription updated successfully.";
            }
            else
            {
                TempData["Error"] = "Something went wrong updating the subscription.";
            }

            return RedirectToAction("Index");
        }
    }
}