using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift_1_Kurs_5_AspnetMVC.Data;
using Uppgift_1_Kurs_5_AspnetMVC.Entities;
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

        public async Task<IActionResult> Index(SchoolClassViewModel model)
        {
            var classlist = new List<SchoolClassViewModel>();
            var currentUser = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var school = _context.SchoolClasses.ToList();
            
            var schoolClass =  _context.SchoolClassStudents.Where(x => x.StudentId == currentUser.Id);
            foreach(var student in schoolClass)
            {
                var user = await _userManager.FindByIdAsync(student.StudentId);
                classlist.Add(new SchoolClassViewModel
                {
                    Id = student.SchoolClassId,
                    StudentId = student.StudentId,
                    Student = user
         
                }); 
            }
            //ViewBag.Students = classlist;
            //var classlist = new List<SchoolClassViewModel>();
            //var users = await _userManager.GetUsersInRoleAsync("Student");
            //var schoolClass = await _context.SchoolClasses
            //    .FirstOrDefault(m => m.Id == id);
            return View(classlist);
        }
    }
}
