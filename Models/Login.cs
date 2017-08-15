using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FYP.Models
{
    public class Login
    {
        [Required(ErrorMessage = "User ID cannot be empty!")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Empty password not allowed!")]
        [DataType(DataType.Password)]
        public string al_password { get; set; }

        public int al_id { get; set; }
        public string al_name { get; set; }
        public int type { get; set; }
        public string al_email { get; set; }

    }
}
