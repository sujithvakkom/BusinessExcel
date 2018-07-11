using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class LineTarget
    {
        public LineTarget(){
            MinAchiveLimit = 100;
            IsBonusLine = false;
            Target = null;
        }
        [Display(Name = "Category Line")]
        public string Catogery { get; set; }

        [Display(Name = "Min Achieve Limit")]
        public Decimal MinAchiveLimit { get; set; }

        [Display(Name = "Target")]
        [DataType(DataType.Currency)]
        public decimal? Target { get; set; }

        [Display(Name = "Bonus")]
        public bool IsBonusLine { get; set; }

        [Display(Name = "Achievement")]
        public Decimal Achievement { get; set; }

        [Display(Name = "Achievement 2")]
        public Decimal AchievementConfirmed { get; set; }

        [Display(Name = "Achievement (%)")]
        public Decimal AchievementPercentage { get; set; }

        public decimal? TotalTarget { get; set; }
        public decimal? AssignedForCurrentUser { get; set; }
    }
}