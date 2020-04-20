using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{

    public partial class TargetAchievementDetailQTR
    {



        [Column("category_id")]
        public int? category_id { get; set; }

        [Column("Category")]
        public string Category { get; set; }


        [Column("target_id")]
        public int? target_id { get; set; }


        [Column("Target_Amt")]
        [Display(Name = "Target")]
        public decimal? Target_amt { get; set; }


        [Column("Achieved_Amt")]
        [Display(Name = "Achieved")]
        public decimal? Achieved_Amt { get; set; }


        [Column("Entered_Amt")]
        [Display(Name = "Entered")]
        public decimal? Entered_Amt { get; set; }


        [Column("has_bonus")]
        [Display(Name = "Bonus Line")]
        public bool has_bonus { get; set; }


        [Column("user_id")]
        public int? user_id { get; set; }

     
        [Column("qtr_start_date")]
        [DataType(DataType.Date)]
        public DateTime? qtr_start_date { get; set; }


        //   public List<TargetTotalView> TargetTotal { get; set; }


    }
}