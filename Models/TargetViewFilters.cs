using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class TargetViewFilters
    {

        [Display(Name = "Target Id")]
        public string target_id { get; set; }

        [Display(Name = "Has Bonus")]
        public bool has_bonus { get; set; }

        [Display(Name = "Target Line Id")]
        public int target_line_id { get; set; }

        [Display(Name = "User Id")]
        public int user_id { get; set; }


        [Display(Name = "Target Qty")]
        public double target_qty { get; set; }

        [Display(Name = "Category Id")]
        public int category_id { get; set; }

        [Display(Name = "Model Id")]
        public int model_id { get; set; }


    
    }
}