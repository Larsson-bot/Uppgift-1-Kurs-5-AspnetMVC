using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift_1_Kurs_5_AspnetMVC.Data;
using Uppgift_1_Kurs_5_AspnetMVC.Models;

namespace Uppgift_1_Kurs_5_AspnetMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly SchoolPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(SchoolPortalDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string id)
        {
            ViewBag.Students = await _userManager.GetUsersInRoleAsync("Student");
            //var classlist = new List<SchoolClassViewModel>();
            //var users = await _userManager.GetUsersInRoleAsync("Student");
            //var schoolClass = await _context.SchoolClasses
            //    .FirstOrDefault(m => m.Id == id);
            return View();
        }
    }
}
