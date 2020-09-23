using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class AcivementReward
    {
        public string Week { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal? UnitIncentive { get; set; }
        public int Quantity { get; set; }
        public decimal? Reward { get; set; }
        public decimal? EnteredReward { get; set; }
        public int? category_id { get; set; }
        public string category { get; set; }
    }
}