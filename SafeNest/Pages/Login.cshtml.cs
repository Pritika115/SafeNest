using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.DataProtection;

namespace SafeNest.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IDataProtector _protector;
        private const string EncryptedPinKey = "SafeNestUserPin";

        [BindProperty]
        public string Pin { get; set; } = "";

        public string Message { get; set; } = "";

        public LoginModel(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("SafeNest.PINProtector");
        }

        public void OnGet()
        {
            // If already logged in, redirect to dashboard
            if (HttpContext.Session.GetString(EncryptedPinKey) != null)
            {
                Response.Redirect("/Index");
            }
        }

        public IActionResult OnPost()
        {
            var storedEncryptedPin = HttpContext.Session.GetString(EncryptedPinKey) ?? "";

            if (string.IsNullOrEmpty(storedEncryptedPin))
            {
                // First-time PIN setup
                HttpContext.Session.SetString(EncryptedPinKey, _protector.Protect(Pin));
                return RedirectToPage("/Index");
            }
            else
            {
                string decryptedPin = _protector.Unprotect(storedEncryptedPin);
                if (Pin == decryptedPin)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    Message = "Incorrect PIN!";
                    return Page();
                }
            }
        }
    }
}
