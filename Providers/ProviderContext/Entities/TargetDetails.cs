﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [Table(name: "user_target", Schema = "sc_salesmanage_test")]
    public class TargetDetails
    {

        [Required]
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Target Line Id")]
        public int target_line_id { get; set; }



        [Display(Name = "Target Id")]
        public int? target_id { get; set; }



        [Display(Name = "Has Bonus")]
        public bool has_bonus { get; set; }

        

        //[Display(Name = "User Id")]
        //public int user_id { get; set; }


        [Display(Name = "Target Qty")]
        public double target_qty { get; set; }

        [Display(Name = "Category")]
        public int? category_id { get; set; }

        [Display(Name = "Model Id")]
        public int? model_id { get; set; }

        [Display(Name = "Target Amt")]
        public decimal value { get; set; }

    }
}