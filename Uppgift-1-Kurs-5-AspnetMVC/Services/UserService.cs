using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift_1_Kurs_5_AspnetMVC.Data;
using Uppgift_1_Kurs_5_AspnetMVC.Models;

namespace Uppgift_1_Kurs_5_AspnetMVC.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task AddUserToRole(ApplicationUser user, string roleName)
        {
            return _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CreateAdminAsync()
        {
            if (!_userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "Account",
                    Email = "Admin@domain.com",
                    UserName = "Admin@domain.com"
                };
                var result = await _userManager.CreateAsync(user, "BytMig123!");
                if (result.Succeeded)
                {
                    if (!_roleManager.Roles.Any())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        await _roleManager.CreateAsync(new IdentityRole("Teacher"));
                        await _roleManager.CreateAsync(new IdentityRole("Student"));
                    };
                }
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }

        public async Task<IdentityResult> CreateNewUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public IEnumerable<IdentityRole> GetAllRolesForUsers()
        {
            var rolelist = new List<IdentityRole>();
            var roles = _roleManager.Roles;

            foreach (var role in roles)
            {
                if (role.Name == "Student")
                {
                    rolelist.Add(role);
                }
                if (role.Name == "Teacher")
                {
                    rolelist.Add(role);
                }
            }
            return rolelist;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var users = _userManager.Users;
            var userlist = new List<UserViewModel>();
           
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();

                userlist.Add(new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = role
                });
            }
            return userlist;
        }
    }
}
