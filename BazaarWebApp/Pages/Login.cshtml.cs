using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Classes;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace BazaarWebApp.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Credential Credential { get; set; }
		public Employee Employee { get; set; }
		public Login Login = new Login ();

        public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

            this.Login = new Login();

            Employee employee = this.Login.AuthenticateUser(this.Credential.Email, this.Credential.Password);

			if (employee == null)
			{
				return Page();
			}

			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Name, employee.Name));
			claims.Add(new Claim("id", employee.Id.ToString()));

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

			return RedirectToPage("/Index");
		}
	}

    public class Credential
    {
        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "format is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "is required")]
        [DataType(DataType.Password)]
        [StringLength(24, ErrorMessage = "must be between {2} and {1} characters", MinimumLength = 2)]
        public string Password { get; set; }
    }
}
