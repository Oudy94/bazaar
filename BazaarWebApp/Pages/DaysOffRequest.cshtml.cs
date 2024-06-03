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
        public List<DaysOffRequest> UserDaysOffRequests { get; private set; }

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
            public string Type { get; set; }
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

            UserDaysOffRequests = _manager.GetDaysOffRequest().Where(r => r.EmployeeId == userId).OrderByDescending(r => r.StartDate).ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "Please correct the form errors and try again.";
                ReloadUserRequests();
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
                ReloadUserRequests();
                return Page();
            }

            _manager.AddDaysOffRequest(CurrentEmployee.Id, temporaryData.StartDate, temporaryData.EndDate, temporaryData.Description, 0, temporaryData.Type);
            ViewData["Message"] = "Your days off request has been successfully submitted.";

            ReloadUserRequests();
            return Page();
        }

        private void ReloadUserRequests()
        {
            int userId = int.Parse(User.FindFirstValue("id"));
            UserDaysOffRequests = _manager.GetDaysOffRequest().Where(r => r.EmployeeId == userId).OrderByDescending(r => r.StartDate).ToList();
        }
    }
}
