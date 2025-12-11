using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeNest.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace SafeNest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string DataFilePath;

        public IndexModel(IWebHostEnvironment env)
        {
            // Correct full physical path to wwwroot/data
            DataFilePath = Path.Combine(env.WebRootPath, "data", "sensorData.json");
        }

        [BindProperty]
        [Required(ErrorMessage = "Sensor Type is required")]
        public string SensorType { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Sensor Value is required")]
        [Range(0, 9999, ErrorMessage = "Enter a valid number between 0 and 9999")]
        public double? SensorValue { get; set; }

        public List<SensorReading> Readings { get; set; } = new();

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedIn")))
                return RedirectToPage("/Login");

            LoadData();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedIn")))
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
            {
                LoadData();
                return Page();
            }

            var allowed = new List<string> { "Motion", "Temperature", "Humidity", "Light" };
            if (!allowed.Contains(SensorType))
            {
                ModelState.AddModelError("SensorType", "Invalid type. Allowed: Motion, Temperature, Humidity, Light.");
                LoadData();
                return Page();
            }

            LoadData();

            Readings.Add(new SensorReading
            {
                SensorType = SensorType,
                Value = SensorValue.Value,
                Time = DateTime.Now
            });

            SaveData();

            return RedirectToPage();
        }

        private void LoadData()
        {
            if (System.IO.File.Exists(DataFilePath))
            {
                var json = System.IO.File.ReadAllText(DataFilePath);
                if (!string.IsNullOrWhiteSpace(json))
                    Readings = JsonSerializer.Deserialize<List<SensorReading>>(json) ?? new();
            }
        }

        private void SaveData()
        {
            // Ensure folder exists
            var folder = Path.GetDirectoryName(DataFilePath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var json = JsonSerializer.Serialize(Readings, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(DataFilePath, json);
        }
    }
}
