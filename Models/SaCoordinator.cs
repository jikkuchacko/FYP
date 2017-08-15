using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class SaCoordinator
    {
        public int SaId { get; set; }
        public string SaName { get; set; }
        public string SaEmail { get; set; }
        public string SaContactNumber { get; set; }
        public int SchoolSchoolId { get; set; }

        public virtual School SchoolSchool { get; set; }
    }
}
