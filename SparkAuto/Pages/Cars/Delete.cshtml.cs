using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    [Authorize]

    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
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
            var carFromDb = _db.Car.FirstOrDefault(c => c.Id == Car.Id);
            if (carFromDb == null) return NotFound();

            _db.Car.Remove(carFromDb);

            await _db.SaveChangesAsync();
            StatusMessage = "Car has been deleted";
            return RedirectToPage("Index", new { userId = Car.UserId });
        }
    }
}
