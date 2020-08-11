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

        public async Task<IActionResult> OnGet(int userPage = 1, string searchEmail = null, string searchName = null, string searchPhone = null)
        {
            UsersListVM = new UsersListViewModel();

            StringBuilder param = new StringBuilder("/Users?userPage=:");
            param.Append("&searchName=");
            if (searchName != null) param.Append(searchName);
            param.Append("&searchEmail=");
            if (searchEmail != null) param.Append(searchEmail);
            param.Append("&searchPhone=");
            if (searchPhone != null) param.Append(searchPhone);

            if (searchEmail != null)
            {
                UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Email.ToLower().Contains(searchEmail)).ToListAsync();
            }
            else
            {
                if (searchName != null)
                {
                    UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Email.ToLower().Contains(searchName)).ToListAsync();
                }
                else
                {
                    if (searchPhone != null)
                    {
                        UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Email.ToLower().Contains(searchPhone)).ToListAsync();
                    }
                    else
                    {
                        UsersListVM.ApplicationUserList = await _db.ApplicationUser.ToListAsync();
                    }
                }
            }

            UsersListVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = userPage,
                ItemsPerPage = SD.PaginationUsersPageSize,
                TotalItems = UsersListVM.ApplicationUserList.Count,
                UrlParam = param.ToString()
            };


            UsersListVM.PagingInfo.UrlParam = param.ToString();

            UsersListVM.ApplicationUserList = UsersListVM.ApplicationUserList.OrderBy(p => p.Name)
                .Skip((userPage - 1) * SD.PaginationUsersPageSize)
                .Take(SD.PaginationUsersPageSize)
                .ToList();

            return Page();
        }
    }

}
