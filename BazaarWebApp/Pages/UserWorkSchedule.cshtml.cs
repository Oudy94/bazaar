using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Classes;
using SharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BazaarWebApp.Pages
{
    public class UserWorkScheduleModel : PageModel
    {
        private readonly EmployeeManager _employeeManager = new EmployeeManager();
        private readonly ShiftManager _shiftManager = new ShiftManager();

        public List<Shift> Shifts { get; private set; }
        public string EmployeeName { get; private set; }
        public DateTime CurrentDate { get; private set; }
        public string ViewMode { get; private set; } = "timeGridWeek";

        public async Task<IActionResult> OnGetAsync(string viewMode = "timeGridWeek", DateTime? currentDate = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            int userId = int.Parse(User.FindFirstValue("id"));
            var currentEmployee = _employeeManager.GetEmployeeById(userId);
            EmployeeName = currentEmployee.Name;

            CurrentDate = currentDate ?? DateTime.Now;
            ViewMode = viewMode;

            Shifts = ViewMode switch
            {
                "dayGridMonth" => _shiftManager.GetEmployeeShiftsOnMonthFromDB(userId, CurrentDate.Month, CurrentDate.Year),
                _ => _shiftManager.GetEmployeeShifts(currentEmployee).Where(s => s.Date.ToDateTime(new TimeOnly()) >= CurrentDate.StartOfWeek(DayOfWeek.Monday) && s.Date.ToDateTime(new TimeOnly()) < CurrentDate.StartOfWeek(DayOfWeek.Monday).AddDays(7)).ToList()
            };

            return Page();
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
