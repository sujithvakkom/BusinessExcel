﻿using System;
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

        }
        [Display(Name = "Category")]
        public string Catogery { get; set; }

        [Display(Name = "Min Achieve Limit")]
        public Decimal MinAchiveLimit { get; set; }

        [Display(Name = "Target")]
        public Decimal Target { get; set; }

        [Display(Name = "Bonus")]
        public bool IsBonusLine { get; set; }

        [Display(Name = "Achievement")]
        public Decimal Achievement { get; set; }

        [Display(Name = "Achievement 2")]
        public Decimal AchievementConfirmed { get; set; }

        [Display(Name = "Achievement (%)")]
        public Decimal AchievementPercentage { get; set; }
    }
}