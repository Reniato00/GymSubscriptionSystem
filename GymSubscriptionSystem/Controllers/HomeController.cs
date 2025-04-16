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

        public async Task<IActionResult> Index(int page = 1, string searchTerm = "")
        {
            int pageSize = 12;
            var customersQuery = await customerService.GetAll(searchTerm.ToLower(), (page-1) * pageSize, pageSize);

            int totalCustomers = await customerService.GetCostumersCount(searchTerm.ToLower());
            int totalPages = (int)Math.Ceiling(totalCustomers / (double)pageSize);

            var viewModel = new IndexViewModel
            {
                Customers = customersQuery,
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
