using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Car Car { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();
            Car = _db.Car.FirstOrDefault(c => c.Id == id);
            if (Car == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var carFromDb = _db.Car.FirstOrDefault(c => c.Id == Car.Id);
            if (carFromDb == null) return NotFound();

            carFromDb.Make = Car.Make;
            carFromDb.Miles = Car.Miles;
            carFromDb.Model = Car.Model;
            carFromDb.Style = Car.Style;
            carFromDb.Year = Car.Year;
            carFromDb.VIN = Car.VIN;

            await _db.SaveChangesAsync();
            StatusMessage = "Car has been updated";
            return RedirectToPage("Index", new { userId = Car.UserId });
        }
    }
}
