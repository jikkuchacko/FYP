﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FYP.Models
{
    public class SendEmail
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
