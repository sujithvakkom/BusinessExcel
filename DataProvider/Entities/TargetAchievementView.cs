using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataProvider.Entities
{
    public class TargetAchievementView
    {
        [Key()]
        [Column("user_id")]
        public int? user_id { get; set; }

        [Column("Category")]
        public string Category { get; set; }



        // [Key, Column(Order = 1)]
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

        [NotMapped]
        [Display(Name = "Month")]
        public string Month { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? start_date { get; set; }


        //  [Key, Column(Order = 4)]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? end_date { get; set; }
    }
}