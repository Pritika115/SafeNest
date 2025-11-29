using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeNest.Models;

namespace SafeNest.Pages
{
    public class IndexModel : PageModel
    {
        private const string EncryptedPinKey = "SafeNestUserPin";

        public List<SensorReading> Readings { get; set; } = new();

        // Temporary in-memory storage (replace with Repository/DB later)
        private static List<SensorReading> AllReadings = new List<SensorReading>
        {
            new SensorReading { Id = 1, Timestamp = DateTime.Now, SensorType = "Motion", Value = 0 },
            new SensorReading { Id = 2, Timestamp = DateTime.Now, SensorType = "Temperature", Value = 25.5 }
        };

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString(EncryptedPinKey) == null)
                return RedirectToPage("/Login");

            Readings = AllReadings;
            return Page();
        }

        public IActionResult OnPostAddReading(string SensorType, double Value)
        {
            if (HttpContext.Session.GetString(EncryptedPinKey) == null)
                return RedirectToPage("/Login");

            int newId = AllReadings.Count + 1;
            AllReadings.Add(new SensorReading
            {
                Id = newId,
                Timestamp = DateTime.Now,
                SensorType = SensorType,
                Value = Value
            });

            Readings = AllReadings;
            return Page();
        }
    }
}
