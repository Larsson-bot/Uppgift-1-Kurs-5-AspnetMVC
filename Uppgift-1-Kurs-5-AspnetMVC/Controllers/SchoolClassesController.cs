using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uppgift_1_Kurs_5_AspnetMVC.Data;
using Uppgift_1_Kurs_5_AspnetMVC.Entities;
using Uppgift_1_Kurs_5_AspnetMVC.Models;

namespace Uppgift_1_Kurs_5_AspnetMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SchoolClassesController : Controller
    {
        private readonly SchoolPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchoolClassesController(SchoolPortalDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolClasses
        public async Task<IActionResult> Index()
        {
      

            var classes = await _context.SchoolClasses.ToListAsync();
            foreach(var schoolclass in classes)
            {
                schoolclass.Teacher = await _userManager.Users.FirstOrDefaultAsync(au => au.Id == schoolclass.TeacherId);
            }

          


            return View(classes);
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var classlist = new List<SchoolClassViewModel>();
            var users = await _userManager.GetUsersInRoleAsync("Student");
            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            schoolClass.Teacher = await _userManager.Users.FirstOrDefaultAsync(au => au.Id == schoolClass.TeacherId);
        
            var school = _context.SchoolClassStudents.ToList();
            foreach (var user in school)
            {
                var studentfind = await _userManager.FindByIdAsync(user.StudentId);
                if(user.SchoolClassId == schoolClass.Id)
                {
                    classlist.Add(new SchoolClassViewModel
                    {

                        Id = schoolClass.Id,
                        TeacherId = schoolClass.TeacherId,
                        StudentId = user.StudentId,
                        Student = studentfind
                    }); ;
                }




            }
            //school.FirstOrDefault(s => s.SchoolClassId == schoolClass.Id)
            ViewBag.ClassInfo = classlist;
            if (schoolClass == null)
                {
                    return NotFound();
                }

                return View(schoolClass);
            }
       

        // GET: SchoolClasses/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            return View();
    
        }

        // POST: SchoolClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName,TeacherId,Created")] SchoolClass schoolClass)
        {

            var checkIfClassNameExists = _context.SchoolClasses.Where(sc => sc.ClassName == schoolClass.ClassName);
            if(checkIfClassNameExists.Count() == 1)
            {
            
                return  RedirectToAction("ClassAlreadyExists", "Admins"); 
            };
           
            if (ModelState.IsValid)
            {
                schoolClass.Id = Guid.NewGuid();
                schoolClass.Created = DateTime.Now;
                _context.Add(schoolClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolClass);
        }

        // GET: SchoolClasses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }
            return View(schoolClass);
        }

        // POST: SchoolClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ClassName,TeacherId,Created")] SchoolClass schoolClass)
        {
            if (id != schoolClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassExists(schoolClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schoolClass);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            _context.SchoolClasses.Remove(schoolClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassExists(Guid id)
        {
            return _context.SchoolClasses.Any(e => e.Id == id);
        }
    }
}
