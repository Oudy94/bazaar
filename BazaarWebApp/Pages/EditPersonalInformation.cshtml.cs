using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedLibrary.Classes;

namespace BazaarWebApp.Pages
{
    public class EditPersonalInformationModel : PageModel
    {
        private readonly EmployeeManager _employeeManager;

        public dynamic CurrentEmployee { get; private set; }

        public dynamic imagePath { get; set; }

        private dynamic passwordChange { get; set; }

        [BindProperty]
        public EmployeeViewModel Input { get; set; }

        public EditPersonalInformationModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }


        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }
            int userId = int.Parse(User.FindFirstValue("id"));
            CurrentEmployee = _employeeManager.GetEmployeeById(userId);
            if (CurrentEmployee == null && CurrentEmployee.Id != id)
            {
                return RedirectToPage("/Index");
            }

            imagePath = CurrentEmployee.Image;

            Input = new EmployeeViewModel
            {
                Id = CurrentEmployee.Id,
                Name = CurrentEmployee.Name,
                Email = CurrentEmployee.Email,
                Address = CurrentEmployee.Address,
                PhoneNumber = CurrentEmployee.PhoneNumber,
                BSN = CurrentEmployee.BSN,
                BankAccount = CurrentEmployee.BankAccount
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            ModelState.Remove("Input.Password");
            ModelState.Remove("Input.Image");
            if (!ModelState.IsValid)
            {
                TempData["AlertMessage"] = "Cannot have empty fields. Please check your input.";
                return RedirectToPage(new { id = id });
            }

            CurrentEmployee = _employeeManager.GetEmployeeById(id);
            if (CurrentEmployee == null)
            {
                return RedirectToPage("/Login");
            }

            bool hasChanged =
                Input.Name != CurrentEmployee.Name ||
                Input.Email != CurrentEmployee.Email ||
                Input.Address != CurrentEmployee.Address ||
                Input.PhoneNumber != CurrentEmployee.PhoneNumber ||
                Input.BSN != CurrentEmployee.BSN ||
                Input.BankAccount != CurrentEmployee.BankAccount ||
                (Input.Password != null && !string.IsNullOrWhiteSpace(Input.Password));
            // || Input.Image != null;

            if (!hasChanged)
            {
                TempData["AlertMessage"] = "No changes were detected in your profile.";
                return RedirectToPage(new { id = id });
            }

            string imagePath = CurrentEmployee.Image;

            string passwordChange = string.IsNullOrEmpty(Input.Password) ? CurrentEmployee.Password : Input.Password;


            _employeeManager.UpdateEmployee(
                CurrentEmployee.Id,
                Input.Name,
                Input.Email,
                passwordChange,
                CurrentEmployee.Role,
                imagePath,
                Input.Address,
                CurrentEmployee.Department,
                CurrentEmployee.HourlyWage,
                true,
                Input.PhoneNumber,
                Input.BSN,
                Input.BankAccount
            );

            return RedirectToPage("/Index");
        }


        private async Task<string> UploadImageToFreeImageHost(IFormFile file, string apiKey)
        {
            using (var client = new HttpClient())
            {
                var form = new MultipartFormDataContent();
                using (var fileStream = file.OpenReadStream())
                {
                    using (var streamContent = new StreamContent(fileStream))
                    {
                        using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                        {
                            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                            form.Add(fileContent, "source", file.FileName);
                            form.Add(new StringContent(apiKey), "key");

                            HttpResponseMessage response = await client.PostAsync("https://freeimage.host/api/1/upload", form);
                            response.EnsureSuccessStatusCode();
                            string responseBody = await response.Content.ReadAsStringAsync();

                            dynamic result = JsonConvert.DeserializeObject(responseBody);
                            if (result.status_code == 200)
                            {
                                return result.image.url;
                            }
                            else
                            {
                                throw new Exception("Error uploading image: " + result.message);
                            }
                        }
                    }
                }
            }
        }

        public class EmployeeViewModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            public string Password { get; set; }

            // public IFormFile Image { get; set; }

            public string Address { get; set; }

            public string PhoneNumber { get; set; }

            public int BSN { get; set; }

            public string BankAccount { get; set; }
        }
    }
}
