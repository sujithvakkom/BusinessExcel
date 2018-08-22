using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class MerchantTargetFilter
    {

        [Display(Name = "Users")]
        public string[] UserName { get; set; }

        [Display(Name = "Select All Users")]
        public bool SelectAllUsers { get; set; }

        [Display(Name = "Period")]
        public string Quarter_Name { get; set; }
    }
}