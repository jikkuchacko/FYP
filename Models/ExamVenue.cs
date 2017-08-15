using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class ExamVenue
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassLevel { get; set; }
        public string TimeslotModuleCode { get; set; }
        public int TimeslotAlLecturerAlId { get; set; }
        public int TimeslotTimeslotId { get; set; }

        public virtual Timeslot TimeslotTimeslot { get; set; }
    }
}
