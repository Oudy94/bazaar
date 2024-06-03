using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Classes;

namespace BazaarWebApp.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly EmployeeManager _employeeManager;
        public Employee CurrentEmployee { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public ProfileModel(ILogger<IndexModel> logger, EmployeeManager employeeManager)
        {
            _logger = logger;
            _employeeManager = employeeManager;
        }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            try
            {
                int userId = int.Parse(User.FindFirstValue("id"));
                CurrentEmployee = _employeeManager.GetEmployeeById(userId);

                if (CurrentEmployee == null)
                {
                    _logger.LogWarning($"No employee found with ID: {userId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee details.");
            }

            return Page();
        }

    }
}
