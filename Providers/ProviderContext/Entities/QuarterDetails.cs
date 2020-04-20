using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class QuarterDetails
    {

        public DateTime? start_date { get; set; }
        public int? user_id { get; set; }

        public  decimal? entered_base_incentive { get; set; }
        public decimal? entered_incentive_amt { get; set; }
    }
}