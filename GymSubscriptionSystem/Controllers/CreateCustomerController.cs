using System.Threading.Tasks;
using Bussines.Services;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    public class CreateCustomerController : Controller
    {
        private readonly ICustomerService customerService;
        public CreateCustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al crear el cliente.";
                return RedirectToAction("Index","Home");
            }

            await customerService.CreateCustomer(model.Name, model.Gender, model.SubscriptionPlanMonths);

            TempData["SuccessMessage"] = "¡Cliente creado exitosamente!";
            return RedirectToAction("Index","Home"); // o donde quieras redirigir
        }
    }
}
