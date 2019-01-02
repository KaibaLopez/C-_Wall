using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Wall.Models
{
    public class LoginUser
    {
        // No other fields!
        [Required]
        [EmailAddress]
        [Display(Name = "email:")]
        public string logemail { get; set; }
        [Required]
        [Display(Name = "password:")]
        public string password { get; set; }
    }
}