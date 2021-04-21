using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.Web.Mvc.CompareAttribute;

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

    public class RegisterModel : LoginModel
    {
        [Required]
        [Display(Name = "Full Name", Prompt = "Full Name", Description = "Full Name", ShortName = "Full Name")]
        [UIHint("Full Name")]
        public string RegFullName { get; set; }

        [Required]
        [Display(Name = "Confirm Password", Prompt = "Confirm Password", Description = "Confirm Password", ShortName = "Confirm Password")]
        [UIHint("Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}