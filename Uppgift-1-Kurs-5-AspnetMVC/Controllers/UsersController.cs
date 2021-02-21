using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles ="Student")]
    public class UsersController : Controller
    {
        private readonly SchoolPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(SchoolPortalDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var classlist = new List<SchoolClassViewModel>();
            var classlist2 = new List<StudentViewModel>();
            var currentUser = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var school = _context.SchoolClasses.ToList();
    
            var schoolClass = _context.SchoolClassStudents.Where(x => x.StudentId == currentUser.Id);
            foreach (var student in schoolClass)
            {
                var user = await _userManager.FindByIdAsync(student.StudentId);
                classlist.Add(new SchoolClassViewModel
                {
                    Id = student.SchoolClassId,
                    StudentId = student.StudentId,
                    Student = user

                });
            }
            var classs = classlist.FirstOrDefault(x => x.StudentId == currentUser.Id);
            if(classs == null)
            {
                return View();
            }
            var selectedclass = school.Where(s => s.Id == classs.Id).FirstOrDefault();

         
            selectedclass.Teacher =  _userManager.Users.FirstOrDefault(au => au.Id == selectedclass.TeacherId);
            ViewBag.Teacher = _userManager.Users.Where(s => s.Id == selectedclass.TeacherId);
            ViewBag.ClassName = school.Where(s => s.Id == classs.Id);
            
            var lsalsda = _context.SchoolClassStudents.Where(x => x.SchoolClassId == classs.Id);
            foreach(var studen in lsalsda)
            {
                var user = await _userManager.FindByIdAsync(studen.StudentId);
                classlist2.Add(new StudentViewModel
                {
                   
                    StudentId = studen.StudentId,
                    Student = user,
                    Id = studen.SchoolClass.Id,
                    GetSchoolClass = selectedclass
                });
            }

             ViewBag.Class = classlist2;
   

            return View(classlist2);
        }
    }
}
