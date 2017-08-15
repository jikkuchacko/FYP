using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class OrgCoordinator
    {
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string OrgEmail { get; set; }
        public string OrgContactNumber { get; set; }
        public int SchoolSchoolId { get; set; }

        public virtual School SchoolSchool { get; set; }
    }
}
