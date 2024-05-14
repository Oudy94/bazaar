using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Classes;
using SharedLibrary.Helpers;
using System.Security.Claims;

namespace BazaarWebApp.Pages
{

    public class UserWorkScheduleModel : PageModel
    {
        private readonly EmployeeManager _employeeManager = new EmployeeManager();
        private readonly DatabaseHelper _databaseHelper = new DatabaseHelper();

        public List<Shift> WorkSchedule;
        public string EmployeeName;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            int userID = int.Parse(User.FindFirstValue("id"));

            var currentEmployee = _employeeManager.GetEmployeeById(userID);

            EmployeeName = currentEmployee.Name;

            WorkSchedule = _databaseHelper.GetEmployeeSchedule30D(userID);

            Console.WriteLine(WorkSchedule.Count);

            return Page();
        }
    }
}
