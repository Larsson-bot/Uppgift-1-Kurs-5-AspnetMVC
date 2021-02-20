using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Uppgift_1_Kurs_5_AspnetMVC.Data;

#nullable disable

namespace Uppgift_1_Kurs_5_AspnetMVC.Entities
{
    public partial class SchoolClass
    {
        public SchoolClass()
        {
            SchoolClassStudents = new HashSet<SchoolClassStudent>();
        }

        public Guid Id { get; set; }
        [Required]
        public string ClassName { get; set; }
        [DisplayName("Teacher")]
        [Required(ErrorMessage = "You must select a Teacher!")]

        public string TeacherId { get; set; }
        public DateTime Created { get; set; }

        public virtual ApplicationUser Teacher { get; set; }




  
        public virtual ICollection<SchoolClassStudent> SchoolClassStudents { get; set; }
    }
}
