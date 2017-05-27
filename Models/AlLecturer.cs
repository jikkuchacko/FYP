using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public partial class AlLecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] Password { get; set; }
    }
}
