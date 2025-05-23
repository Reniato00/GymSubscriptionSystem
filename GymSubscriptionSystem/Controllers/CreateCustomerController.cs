﻿using Bussines.Services;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    [Authorize]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al crear el cliente.";
                return RedirectToAction("Index","Home");
            }

            await customerService.CreateCustomer(model.Name, model.Gender, model.SubscriptionPlanMonths, User.Identity?.Name ?? string.Empty);

            TempData["SuccessMessage"] = "¡Cliente creado exitosamente!";
            return RedirectToAction("Index","Home"); // o donde quieras redirigir
        }
    }
}
