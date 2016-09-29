using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Models
{
    public class EditUserProfile
    {

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Full Name")]
        public string UserFullName { get; set; }

        [Display(Name = "New Password")]
        [Required]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords should match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Current Password")]
        [Required]
        public string CurrentPassword { get; set; }
    }
}