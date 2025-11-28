using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeNest.Data;
using SafeNest.Models;
using System.Collections.Generic;

namespace SafeNest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Repository _repo = new Repository("Data Source=safenest.db");

        public List<SensorReading> Readings { get; set; } = new();

        public void OnGet()
        {
            Readings = _repo.GetAll();
        }

        public IActionResult OnPostAdd()
        {
            var sample = new SensorReading
            {
                Timestamp = DateTime.Now,
                SensorType = "Temperature",
                Value = 22.5
            };

            _repo.Add(sample);

            Readings = _repo.GetAll();

            return Page();
        }
    }
}
