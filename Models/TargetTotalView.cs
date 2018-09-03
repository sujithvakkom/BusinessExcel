using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class TargetTotalView
    {

        [Key()]
        public decimal? TotalTargetPerc { get; set; }
        public decimal? TotalIncAmt { get; set; }

        public decimal? TotalEnteredIncAmt { get; set; }
      
        public decimal? BaseIncentive { get; set; }

        public int? LineAch { get; set; }
        public decimal? TotalLineAchAccAmt { get; set; }

        public decimal? totalBonusLinePerc { get; set; }
        public decimal? TotalBonusLineAmt { get; set; }

       
        public decimal? TotalEnteredBaseIncentive { get; set; }

        [NotMapped]
        [Display(Name = "Status")]
        public int target_status { get; set; }

      

        [NotMapped]
        public int? target_id { get; set; }

        [NotMapped]
        public int? user_id { get; set; }
    }
}