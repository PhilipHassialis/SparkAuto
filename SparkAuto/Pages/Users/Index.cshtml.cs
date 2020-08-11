using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Models.ViewModels;
using SparkAuto.Utility;

namespace SparkAuto.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public UsersListViewModel UsersListVM { get; set; }

        public async Task<IActionResult> OnGet(int userPage = 1)
        {
            var usersList = await _db.ApplicationUser.ToListAsync();
            UsersListVM = new UsersListViewModel()
            {
                ApplicationUserList = usersList,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = userPage,
                    ItemsPerPage = SD.PaginationUsersPageSize,
                    TotalItems = usersList.Count,
                    UrlParam = "/Users?userPage=:"

                }
            };
            UsersListVM.ApplicationUserList = UsersListVM.ApplicationUserList.OrderBy(p => p.Name)
                .Skip((userPage - 1) * SD.PaginationUsersPageSize)
                .Take(SD.PaginationUsersPageSize)
                .ToList();

            return Page();
        }
    }

}
