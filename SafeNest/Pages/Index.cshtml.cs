using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeNest.Data;
using SafeNest.Models;

namespace SafeNest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Repository _repo;

        public IndexModel(Repository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public string NewSensorType { get; set; } = string.Empty;

        [BindProperty]
        public double NewSensorValue { get; set; }

        public List<SensorReading> Readings { get; set; } = new();

        public void OnGet()
        {
            Readings = _repo.GetAll();
        }

        public IActionResult OnPost()
        {
            var reading = new SensorReading
            {
                Timestamp = DateTime.Now,
                SensorType = NewSensorType,
                Value = NewSensorValue
            };

            _repo.Add(reading);

            return RedirectToPage();
        }
    }
}
