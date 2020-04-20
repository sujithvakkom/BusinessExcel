using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class CreateUser
    {
        [Required(ErrorMessage = "Username is Required")]
        [Display(Name = " ")]
        [Column("user_name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = " ")]
        [Column("first_name")]
        public string FirstName { get; set; }
        [Display(Name = " ")]
        [Column("second_name")]
        public string SecondName { get; set; }
        [Display(Name = " ")]
        [Column("display_name")]
        public string DisplayName { get; set; }
        [Display(Name = " ")]
        [Column("is_active")]
        public int IsActive { get; set; }
        [Display(Name = " ")]
        [Column("user_group_id")]
        public int USERGROUPID { get; set; }

        [Display(Name = " ")]
        [Column("group_name")]
        public string USERGROUPNAME { get; set; }

        [Display(Name = " ")]
        [Column("email")]
        public string Email { get; set; }
        [Display(Name = " ")]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Display(Name = "ss ")]
       // [Column("role_name")]
        public string RoleName { get; set; }

        [Display(Name = " ")]
        [Column("contact_number")]
        public string ContactNumber { get; set; }
        [Display(Name = " ")]
        [Column("emp_code_lookup")]
        public string EmpCodeLookup { get; set; }

        [Required]
        [Display(Name = " ")]
        [Column("password")]
        public string Password { get; set; }
    }
}