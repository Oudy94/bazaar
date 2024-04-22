using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Classes;

namespace BazaarWebApp.Pages
{
    [Authorize]
    public class DaysOffRequestModel : PageModel
    {
        private readonly DaysOffRequestManager _manager = new DaysOffRequestManager();
        private readonly EmployeeManager _employeeManager;
        public dynamic CurrentEmployee { get; private set; }

        [BindProperty]
        public TemporaryData temporaryData { get; set; }

        public DaysOffRequestModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public class TemporaryData
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Description { get; set; }
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }
            int userId = int.Parse(User.FindFirstValue("id"));
            CurrentEmployee = _employeeManager.GetEmployeeById(userId);
            if (CurrentEmployee == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "Please correct the form errors and try again.";
                return Page();
            }
            int userId = int.Parse(User.FindFirstValue("id"));
            CurrentEmployee = _employeeManager.GetEmployeeById(userId);
            if (CurrentEmployee == null)
            {
                return RedirectToPage("/Index");
            }
            if (temporaryData.StartDate > temporaryData.EndDate)
            {
                ViewData["Error"] = "End Date must be greater than or equal to Start Date.";
                return Page();
            }

            _manager.AddDaysOffRequest(CurrentEmployee.Id, temporaryData.StartDate, temporaryData.EndDate, temporaryData.Description);
            ViewData["Message"] = "Your days off request has been successfully submitted.";

            return Page();
        }
    }
}
