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
      public string Password { get; set; }

      public int Id { get; set; }
      public string Name { get; set; }

   }
}
