using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Utility;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();
            ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == id);

            if (ApplicationUser == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userFromDb = await _db.ApplicationUser.FirstOrDefaultAsync(s => s.Id == ApplicationUser.Id);

            if (userFromDb == null) return NotFound();

            _db.ApplicationUser.Remove(userFromDb);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
