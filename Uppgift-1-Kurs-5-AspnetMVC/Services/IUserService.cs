using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift_1_Kurs_5_AspnetMVC.Data;
using Uppgift_1_Kurs_5_AspnetMVC.Models;

namespace Uppgift_1_Kurs_5_AspnetMVC.Services
{
    public interface IUserService
    {
        Task CreateAdminAsync();

        IEnumerable<IdentityRole> GetAllRoles();

        Task<IEnumerable<UserViewModel>> GetAllUsers();

        Task AddUserToRole(ApplicationUser user,string roleName);

        Task<IdentityResult> CreateNewUserAsync(ApplicationUser user, string password);

        IEnumerable<IdentityRole> GetAllRolesForUsers();
    }
}
