using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Classes;
using SharedLibrary.Helpers;
using System;
using System.Security.Claims;

namespace BazaarWebApp.Pages
{

    public class UserWorkScheduleModel : PageModel
    {
        private readonly EmployeeManager _employeeManager = new EmployeeManager();
        private readonly DatabaseHelper _databaseHelper = new DatabaseHelper();

        public List<Shift> ImproperWorkSchedule;

        public List<(DateOnly, SharedLibrary.Enums.ShiftTypeEnum)> ProperSchedule = new List<(DateOnly, SharedLibrary.Enums.ShiftTypeEnum)>();


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

            ImproperWorkSchedule = _databaseHelper.GetEmployeeSchedule30D(userID);

            foreach (Shift shift in ImproperWorkSchedule)
            {
                // Iterate through the week days from Monday to Friday
                for (int i = 0; i < 5; i++)
                {
                    {
                        ProperSchedule.Add((shift.Date.AddDays(i), shift.ShiftType)); ;
                    }
                }
            }

            return Page();
        }
    }
}
