using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FYP.Models
{
    public partial class AssociateLecturer
    {
        public AssociateLecturer()
        {
            LectSlot = new HashSet<LectSlot>();
            Timeslot = new HashSet<Timeslot>();
        }

        public int AlId { get; set; }
        public string AlName { get; set; }
        public string AlEmail { get; set; }
        public string AlContactNumber { get; set; }
        public string AlSchool { get; set; }
        public string type { get; set; }
        public int SchoolSchoolId { get; set; }

        public virtual ICollection<LectSlot> LectSlot { get; set; }
        public virtual ICollection<Timeslot> Timeslot { get; set; }
        public virtual School SchoolSchool { get; set; }
    }
}
