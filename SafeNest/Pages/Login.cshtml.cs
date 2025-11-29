using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SafeNest.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Pin { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        private const string CorrectPin = "1234";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Pin == CorrectPin)
            {
                HttpContext.Session.SetString("LoggedIn", "true");
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Incorrect PIN!";
            return Page();
        }
    }
}
