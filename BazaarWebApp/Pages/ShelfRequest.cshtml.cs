using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Classes;

namespace BazaarWebApp.Pages
{
    public class ShelfRequestModel : PageModel
    {
		private readonly StockManager _stockManager;

		public dynamic CurrentEmployee { get; private set; }
		private readonly EmployeeManager _employeeManager;

		public ShelfRequestModel(StockManager stockManager, EmployeeManager employeeManager)
		{
			_stockManager = stockManager;
			_employeeManager = employeeManager;
		}

		public class TemporaryDataShelfRequest
		{
			public int ItemId { get; set; }
			public int Quantity { get; set; }

		}

		[BindProperty]
		public TemporaryDataShelfRequest temporaryDataShelfRequest { get; set; }

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
			if (temporaryDataShelfRequest.Quantity < 1)
			{
				ViewData["Error"] = "Invalid quantity.";
				return Page();
			}
			if (!_stockManager.DataBaseContainsId(temporaryDataShelfRequest.ItemId))
			{
                ViewData["Error"] = "Product not found: no matching ID.";
                return Page();
            }
			_stockManager.AddShelfRequest(temporaryDataShelfRequest.ItemId, temporaryDataShelfRequest.Quantity, SharedLibrary.Enums.ShelfRequestType.INVENTORY);
			ViewData["Message"] = "Your shelf request has been successfully submitted.";

			return Page();
		}
	}
}
