using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Classes;
using System.Threading.Tasks;

namespace BazaarWebApp.Pages
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly EmployeeManager _employeeManager;

        public Employee CurrentEmployee { get; set; }

        public DashboardModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string userEmail = "john@test.com";
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Login");
            }

            CurrentEmployee = _employeeManager.GetEmployeeByEmail(userEmail);
            if (CurrentEmployee == null)
            {
                return NotFound("Employee not found.");
            }

            return Page();
        }
    }
}
