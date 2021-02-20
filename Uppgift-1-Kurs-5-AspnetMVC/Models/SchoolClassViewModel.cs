using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift_1_Kurs_5_AspnetMVC.Data;
using Uppgift_1_Kurs_5_AspnetMVC.Entities;

namespace Uppgift_1_Kurs_5_AspnetMVC.Models
{
    public class SchoolClassViewModel
    {
        public Guid Id { get; set; }
        public string TeacherId { get; set; }
        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }

    }
}
