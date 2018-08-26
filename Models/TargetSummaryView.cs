using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class TargetSummaryView
    {

        [Key()]


        public decimal? TotalTarget { get; set; }
        public decimal? TotalAchieved { get; set; }


        public decimal? TotalTargetPerc { get; set; }
        public decimal? TotalIncAmt { get; set; }

        public decimal? TotalEnteredIncAmt { get; set; }
      
        public decimal? BaseIncentive { get; set; }

        public int? LineAch { get; set; }
        public decimal? line_achieved_amt { get; set; }

        public decimal? bonus_lines { get; set; }
        public decimal? bonus_line_amt { get; set; }

       
        public decimal? TotalEnteredBaseIncentive { get; set; }


        [Display(Name = "Status")]
        public int target_status_id { get; set; }



        public string target_status { get; set; }


        [NotMapped]
        public int? target_id { get; set; }

        [NotMapped]
        public int? user_id { get; set; }

        [NotMapped]
        [Display(Name = "Staff Code")]
        public string user_name { get; set; }

        public int? roster_id { get; set; }

        [Display(Name = "Location")]
        public int? location_id { get; set; }

        public string roster_name { get; set; }
        public string location_name { get; set; }


        public decimal? TotalValue { get; set; }
    }
}