using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using SharedLibrary.Classes;

namespace BazaarWebApp.Pages
{
    public class DaysOffRequestModel : PageModel
    {
        private readonly DaysOffRequestManager _manager= new DaysOffRequestManager();


        [BindProperty]
        public TemporaryData temporaryData { get; set; }

        public class TemporaryData 
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Description { get; set; }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (temporaryData.StartDate <= temporaryData.EndDate)
            {
                _manager.AddDaysOffRequest(1, temporaryData.StartDate, temporaryData.EndDate, temporaryData.Description);
            }
            
            return Page();
        }
    }
}
