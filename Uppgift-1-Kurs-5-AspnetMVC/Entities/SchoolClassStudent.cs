using System;
using System.Collections.Generic;
using Uppgift_1_Kurs_5_AspnetMVC.Data;

#nullable disable

namespace Uppgift_1_Kurs_5_AspnetMVC.Entities
{
    public partial class SchoolClassStudent
    {
        public string StudentId { get; set; }
        public Guid SchoolClassId { get; set; }



        public virtual SchoolClass SchoolClass { get; set; }
    }
}
