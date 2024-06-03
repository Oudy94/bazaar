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
    public class IndexModel : PageModel
    {
        private readonly EmployeeManager _employeeManager;
        private readonly ShiftManager _shiftManager;
        private readonly DaysOffRequestManager _daysOffRequestManager;

        public Employee CurrentEmployee { get; set; }
        public List<Shift> WeeklyShifts { get; private set; }
        public List<DaysOffRequest> RecentDaysOffRequests { get; private set; }
        public DateTime CurrentDate { get; private set; }

        public IndexModel(EmployeeManager employeeManager, ShiftManager shiftManager, DaysOffRequestManager daysOffRequestManager)
        {
            _employeeManager = employeeManager;
            _shiftManager = shiftManager;
            _daysOffRequestManager = daysOffRequestManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            int userId = int.Parse(User.FindFirstValue("id"));
            CurrentEmployee = _employeeManager.GetEmployeeById(userId);
            CurrentDate = DateTime.Now;

            WeeklyShifts = _shiftManager.GetEmployeeShifts(CurrentEmployee)
                .Where(s => s.Date.ToDateTime(new TimeOnly()) >= CurrentDate.StartOfWeek(DayOfWeek.Monday)
                         && s.Date.ToDateTime(new TimeOnly()) < CurrentDate.StartOfWeek(DayOfWeek.Monday).AddDays(7))
                .ToList();

            RecentDaysOffRequests = _daysOffRequestManager.GetDaysOffRequest()
                .Where(r => r.EmployeeId == userId)
                .OrderByDescending(r => r.StartDate)
                .Take(3)
                .ToList();

            return Page();
        }
    }
}
