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
    [Authorize(Roles = "Admin")]
    public class SchoolClassStudentsController : Controller
    {
        private readonly SchoolPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchoolClassStudentsController(SchoolPortalDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolClassStudents
        public async Task<IActionResult> Index()
        {
            //ViewBag.Students = await _userManager.GetUsersInRoleAsync("Student");
            var studentlist = new List<StudentViewModel>();
            var students = await _userManager.GetUsersInRoleAsync("Student");

            var school = _context.SchoolClassStudents.ToList();
            foreach (var user in school)
            {
           
                var studentfind = await _userManager.FindByIdAsync(user.StudentId);
                if (user.SchoolClassId == Guid.Empty)
                {
                  
                }
                else
                {
                    var classname = _context.SchoolClasses.Where(x => x.Id == user.SchoolClassId);

                    studentlist.Add(new StudentViewModel
                    {
                        GetSchoolClass = classname.FirstOrDefault(x => x.Id == user.SchoolClassId),
                        Id = user.SchoolClassId,
                        StudentId = user.StudentId,
                        Student = studentfind
                    }) ;
                }
                
            }
            ViewBag.Students = studentlist;
          
            var schoolPortalDbContext = _context.SchoolClassStudents.Include(s => s.SchoolClass);


            return View(await schoolPortalDbContext.ToListAsync());
        }
        // GET: SchoolClassStudents/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Student = _userManager.Users.Where(s => s.Id == id);
            var schoolClassStudent = await _context.SchoolClassStudents
                .Include(s => s.SchoolClass)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (schoolClassStudent == null)
            {
                return NotFound();
            }

            return View(schoolClassStudent);
        }

        // GET: SchoolClassStudents/Create
        public async Task<IActionResult> Create()
        {
            var students = await _userManager.GetUsersInRoleAsync("Student");
            var school = _context.SchoolClassStudents.ToList();
            var users = students.Where(x => !school.Any(z => z.StudentId == x.Id)).ToList();
  
            if(users.Count() == 0)
            {
                return RedirectToAction("NoStudentError", "Admins");
            }
          
            ViewData["StudentId"] = new SelectList(users, "Id", "DisplayName","Select a Student");
            ViewData["SchoolClassId"] = new SelectList(_context.SchoolClasses, "Id", "ClassName");
            return View();
        }

        // POST: SchoolClassStudents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,SchoolClassId")] SchoolClassStudent schoolClassStudent)
        {


            if (ModelState.IsValid)
            {

                _context.Add(schoolClassStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolClassId"] = new SelectList(_context.SchoolClasses, "Id", "ClassName", schoolClassStudent.SchoolClassId);
            return View(schoolClassStudent);
        }

        // GET: SchoolClassStudents/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Student = _userManager.Users.Where(s => s.Id == id);
            var schoolClassStudent = await _context.SchoolClassStudents.FindAsync(id);
            if (schoolClassStudent == null)
            {
                return NotFound();
            }
            ViewData["SchoolClassId"] = new SelectList(_context.SchoolClasses, "Id", "ClassName", schoolClassStudent.SchoolClassId);
            return View(schoolClassStudent);
        }

        // POST: SchoolClassStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,SchoolClassId")] SchoolClassStudent schoolClassStudent)
        {
            if (id != schoolClassStudent.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolClassStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassStudentExists(schoolClassStudent.StudentId))
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
            ViewData["SchoolClassId"] = new SelectList(_context.SchoolClasses, "Id", "ClassName", schoolClassStudent.SchoolClassId);
            return View(schoolClassStudent);
        }

        // GET: SchoolClassStudents/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Students = _userManager.Users.Where(s => s.Id == id);
            var schoolClassStudent = await _context.SchoolClassStudents
                .Include(s => s.SchoolClass)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (schoolClassStudent == null)
            {
                return NotFound();
            }

            return View(schoolClassStudent);
        }

        // POST: SchoolClassStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var schoolClassStudent = await _context.SchoolClassStudents.FindAsync(id);
            _context.SchoolClassStudents.Remove(schoolClassStudent);
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassStudentExists(string id)
        {
            return _context.SchoolClassStudents.Any(e => e.StudentId == id);
        }
    }
}
