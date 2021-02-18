using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift_1_Kurs_5_AspnetMVC.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        [Column(TypeName ="nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";
    }
}
