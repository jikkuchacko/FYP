using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class Timeslot
    {
        public int timeslot_id { get; set; }
        public string module_schoolcentre { get; set; }
        public DateTime? examDate { get; set; }
        public DateTime? start_time { get; set; }
        public int duration_of_exam { get; set; }
        public int? associate_lecturer_al_id { get; set; }
        public int? module_module_id { get; set; }
        public string moduleModuleCode { get; set; }
        public string AssociateLecturerAl { get; set; }
        public string ModuleModule { get; set; }
        public int ExamVenue { get; set; }
    }
}
