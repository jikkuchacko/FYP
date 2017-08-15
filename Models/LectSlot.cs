using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FYP.Models
{
    public partial class LectSlot
    {
        public int PreferredTimeslotId { get; set; }
        public int TimeslotTimeslot { get; set; }
        public DateTime RequestTime { get; set; }
        public int AssociateLecturerAlId { get; set; }

        [Required(ErrorMessage = "Please select your date!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        public DateTime? date { get; set; }

        [Required(ErrorMessage = "Please select your starting time!")]
        [Range(1, 5, ErrorMessage = "You can only select from 1st to 5th choice!")]
        public int prefered_time { get; set; }

        public DateTime? request_time { get; set; } = DateTime.Now;

        public virtual AssociateLecturer AssociateLecturerAl { get; set; }
        public virtual Timeslot TimeslotTimeslotNavigation { get; set; }
    }
}
