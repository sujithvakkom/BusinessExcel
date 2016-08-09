﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email", Prompt = "Email", Description = "Email", ShortName = "Email")]
        [UIHint("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel //: LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Email", Description = "Email", ShortName = "Email")]
        [UIHint("Email")]
        public string RegEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegPassword { get; set; }
        [Required]
        [Display(Name = "Full Name", Prompt = "Full Name", Description = "Full Name", ShortName = "Full Name")]
        [UIHint("Full Name")]
        public string RegFullName { get; set; }

        [Required]
        [Display(Name = "Confirm Password", Prompt = "Confirm Password", Description = "Confirm Password", ShortName = "Confirm Password")]
        [UIHint("Confirm Password")]
        [Compare("RegConfirmPassword")]
        public string RegConfirmPassword { get; set; }
    }
}