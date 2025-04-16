using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    public class ExpiredPeopleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}