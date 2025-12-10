using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SafeNest.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Pin { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        // You can change this PIN to whatever you want
        private const string CorrectPin = "1234";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Pin))
            {
                ErrorMessage = "Please enter your PIN.";
                return Page();
            }

            if (Pin.Length != 4 || !Pin.All(char.IsDigit))
            {
                ErrorMessage = "PIN must be exactly 4 digits.";
                return Page();
            }

            if (Pin == CorrectPin)
            {
                HttpContext.Session.SetString("LoggedIn", "true");
                return RedirectToPage("/Index"); // redirect to home page
            }

            ErrorMessage = "Incorrect PIN!";
            return Page();
        }
    }
}
