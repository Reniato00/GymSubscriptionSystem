using Bussines.Services;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymSubscriptionSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerService customerService;

        public HomeController(ILogger<HomeController> logger, ICustomerService customerService)
        {
            _logger = logger;
            this.customerService = customerService;
        }

        public IActionResult Index(int page = 1, string searchTerm = "")
        {
            int pageSize = 12;  // Cuántos elementos mostrar por página
            var customersQuery = customerService.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                customersQuery = customersQuery.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalCustomers = customersQuery.Count();
            int totalPages = (int)Math.Ceiling(totalCustomers / (double)pageSize);

            var customers = customersQuery
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new IndexViewModel
            {
                Customers = customers,
                CurrentPage = page,
                TotalPages = totalPages,
                SearchTerm = searchTerm
            };
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
