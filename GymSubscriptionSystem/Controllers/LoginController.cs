using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}