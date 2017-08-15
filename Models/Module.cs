using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class Module
    {
        public Module()
        {
            Timeslot = new HashSet<Timeslot>();
        }

        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int? NumberOfStudents { get; set; }
        public int? NumberOfStaff { get; set; }
        public int ModuleId { get; set; }
        public int SchoolSchoolId { get; set; }

        public virtual ICollection<Timeslot> Timeslot { get; set; }
        public virtual School SchoolSchool { get; set; }
    }
}
