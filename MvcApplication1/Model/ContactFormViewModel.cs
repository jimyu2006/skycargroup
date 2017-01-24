﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Model
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage="Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress]
        public string Email { get; set; }
        
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter message")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Please enter subject")]
        public string Subject { get; set; }
    }
}