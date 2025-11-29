using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeNest.Models;
using System.ComponentModel.DataAnnotations;

namespace SafeNest.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Sensor Type is required")]
        public string SensorType { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Sensor Value is required")]
        [Range(0, 9999, ErrorMessage = "Enter a valid number between 0 and 9999")]
        public double? SensorValue { get; set; }  // decimal values allowed

        public static List<SensorReading> Readings { get; set; } = new List<SensorReading>();

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedIn")))
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedIn")))
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validate sensor type
            var allowedSensors = new List<string> { "Motion", "Temperature", "Humidity", "Light" };
            if (!allowedSensors.Contains(SensorType))
            {
                ModelState.AddModelError("SensorType", "Invalid sensor type. Allowed: Motion, Temperature, Humidity, Light.");
                return Page();
            }

            if (SensorValue == null)
            {
                ModelState.AddModelError("SensorValue", "Sensor value is required.");
                return Page();
            }

            // Add reading
            Readings.Add(new SensorReading
            {
                SensorType = SensorType,
                Value = SensorValue.Value,
                Time = DateTime.Now
            });

            return RedirectToPage();
        }
    }
}
