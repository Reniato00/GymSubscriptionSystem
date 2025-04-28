using Bussines.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSubscriptionSystem.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IInstructorService instructorService;
        public ProfileController(IInstructorService instructorService)
        {
            this.instructorService = instructorService ?? throw new ArgumentNullException(nameof(instructorService));
        }
        
        public async Task<IActionResult> Index()
        {
            string? name = Request.Query["name"];
            var instructor = await instructorService.GetInstructor(name ?? string.Empty);
            var vm = new ProfileViewModel
            {
                Id = instructor?.Id,
                Name = instructor?.Name,
                Password = instructor?.Password,
                Active = instructor?.IsActive ?? false,
                CreationDate = instructor?.CreatedAt.ToString("yyyy-MM-dd"),
            };
            return View(vm);
        }
    }
}