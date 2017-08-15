using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class School
    {
        public School()
        {
            AssociateLecturer = new HashSet<AssociateLecturer>();
            Module = new HashSet<Module>();
            OrgCoordinator = new HashSet<OrgCoordinator>();
            SaCoordinator = new HashSet<SaCoordinator>();
        }

        public int SchoolId { get; set; }
        public string SchoolDepartment { get; set; }
        public string SchoolName { get; set; }

        public virtual ICollection<AssociateLecturer> AssociateLecturer { get; set; }
        public virtual ICollection<Module> Module { get; set; }
        public virtual ICollection<OrgCoordinator> OrgCoordinator { get; set; }
        public virtual ICollection<SaCoordinator> SaCoordinator { get; set; }
    }
}
