using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift_1_Kurs_5_AspnetMVC.Data;
using Uppgift_1_Kurs_5_AspnetMVC.Models;
using Uppgift_1_Kurs_5_AspnetMVC.Services;

namespace Uppgift_1_Kurs_5_AspnetMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminsController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminsController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.Users = _userManager.Users;
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Roles = _userService.GetAllRolesForUsers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userService.CreateNewUserAsync(user, model.Password);
                var users = await _userManager.GetUsersInRoleAsync("Admin");
                if (result.Succeeded)
                {
                    //if (model.Role == null || model.Role == "Admin")
                    //{
                    //    if (users.Count < 1)
                    //    {
                    //        await _userService.AddUserToRole(user, model.Role);
                    //    }
                    //    else
                    //    {
                    //        await _userService.AddUserToRole(user, "Student");
                    //    }

                    //}
                    //else
                    //        await _userService.AddUserToRole(user, model.Role);
                    await _userService.AddUserToRole(user, model.Role);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return RedirectToAction("Index");
            }


            return View();
        }
    }
}
