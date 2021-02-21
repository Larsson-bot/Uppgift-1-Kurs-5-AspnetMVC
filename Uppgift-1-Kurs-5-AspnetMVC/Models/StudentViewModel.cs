using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift_1_Kurs_5_AspnetMVC.Data;

namespace Uppgift_1_Kurs_5_AspnetMVC.Models
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }
    }
}
