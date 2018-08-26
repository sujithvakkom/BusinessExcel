using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{ 
    public class GlobalSettings
    {
        /*
         
        [Display(Name ="Line")]
        [Required]
        [Column("category_id", Order = 1)]
        public int category_id { get; set; }
             */
        [Display(Name = "Line Achivement Acc.")]
        [Required]
        [Column("line_achieve_acc", Order = 1)]
        public decimal line_achieve_acc { get; set; }

        [Display(Name = "Bonus Achivement Acc.")]
        [Required]
        [Column("bonus_achieve_acc", Order = 2)]
        public decimal bonus_achieve_acc { get; set; }

        [Display(Name = "Minimum Total Achievement")]
        [Required]
        [Column("min_total_achievement", Order = 3)]
        public decimal min_total_achievement { get; set; }

        [Display(Name = "Minimum Line Achievement")]
        [Required]
        [Column("min_line_achievement", Order = 4)]
        public decimal min_line_achievement { get; set; }

        [Display(Name = "Minimum Bonus Achievement")]
        [Required]
        [Column("min_bonus_achievement", Order = 5)]
        public decimal min_bonus_achievement { get; set; }

        [Display(Name = "Base Incentive Cap.")]
        [Required]
        [Column("cap_base_incentive", Order = 6)]
        public decimal cap_base_incentive { get; set; }

        [Display(Name = "Enabled")]
        [Required]
        [Column("enabled", Order = 7)]
        public bool enabled { get; set; }

        [Display(Name = "Date")]
        [Required]
        [Column("date", Order = 8)]
        public DateTime date { get; set; }

        [Display(Name = "Base Incentive Pct.")]
        [Required]
        [Column("base_incentive_pct", Order = 9)]
        public decimal base_incentive_pct { get; set; }

        [Display(Name = "Global Base Incentive Acc.")]
        [Required]
        [Column("bonus_achieve_acc", Order = 10)]
        public decimal global_base_incentive_cap { get; set; }

        [Display(Name = "Global Acc. Factor")]
        [Required]
        [Column("global_acc_factor", Order = 11)]
        public decimal global_acc_factor { get; set; }
    }
}