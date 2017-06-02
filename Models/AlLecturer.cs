using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class AlLecturer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string School { get; set; }
        public byte[] Password { get; set; }
        public int Type { get; set; }
    }
}
