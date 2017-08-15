using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FYP.Models
{
    public class Availability
    {
        [Required(ErrorMessage = "Please select your date!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format!")]
        public DateTime? timeslot { get; set; }

        [Required(ErrorMessage = "Please select your starting time!")]
        [Range(1, 5, ErrorMessage = "Time preferred must be between 1 to 5!")]
        public int request_time { get; set; }

        public DateTime? submission_time { get; set; }

    }
}