using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class TargetTotalView
    {

        [Key()]
        public decimal? TotalTargetPerc { get; set; }
        public decimal? TotalIncAmt { get; set; }

        public int? LineAch { get; set; }
        public decimal? TotalLineAchAccAmt { get; set; }

        public decimal? totalBonusLinePerc { get; set; }
        public decimal? TotalBonusLineAmt { get; set; }



    }
}