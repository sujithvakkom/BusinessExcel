using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public partial class UserViewFilters
    {

        [Display(Name = "user_id")]
        public int user_id { get; set; }

        
        [Display(Name = "Staff Code")]
        public string user_name { get; set; }


        [Display(Name = "First Name")]
        public string first_name { get; set; }


        [Display(Name = "Second Name")]
        public string second_name { get; set; }


        [Display(Name = "display_name")]
        public string display_name { get; set; }


        [Display(Name = "create_date")]
        public DateTime create_date { get; set; }

        [Display(Name = "end_date")]
        public DateTime end_date { get; set; }

        [Display(Name = "is_active")]
        public int is_active { get; set; }


        [Display(Name = "email")]
        public string email { get; set; }

        [Display(Name = "role_id")]
        public int role_id { get; set; }



        [Display(Name = "contact_number")]
        public string contact_number { get; set; }


        [Display(Name = "emp_code_lookup")]
        public string emp_code_lookup { get; set; }



    }
}