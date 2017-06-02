using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class Timeslot
    {
        public string ModuleCode { get; set; }
        public string ModuleSchool { get; set; }
        public DateTime? Date { get; set; }
        public string MsaVenue { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? AlId { get; set; }
    }
}
