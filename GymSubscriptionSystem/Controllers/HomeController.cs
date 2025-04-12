using Bussines.Services;
using GymSubscriptionSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

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
            int pageSize = 12;  // Cuantos elementos mostrar por p�gina
            var customersQuery = await customerService.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var filteredCustomers = customersQuery.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()));
                customersQuery = filteredCustomers.ToList();
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
