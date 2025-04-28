using System.Security.Claims;
using Bussines.Services;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IInstructorService instructorService;
        public LoginController(IInstructorService instructorService)
        {
            this.instructorService = instructorService ?? throw new ArgumentNullException(nameof(instructorService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var instructor = await instructorService.GetInstructor(vm.Username, vm.Password);
                if(instructor != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, instructor.Name),
                        new Claim("InstructorId", instructor.Id!) // Puedes agregar más info aquí
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Invalid username or password.";
                    return RedirectToAction("Index", "Login");
                }
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Perform logout logic here
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }
    }
}