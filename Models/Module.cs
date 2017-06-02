using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class Module
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int? NumberOfStrudents { get; set; }
        public int? NumberOfStaff { get; set; }
        public string SchoolId { get; set; }
    }
}
