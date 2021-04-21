using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
    public class DeliveryStatusView
    {
        [Display(Name = "OrderNumber")]
        public String OrderNumber { get; set; }

        [Display(Name = "TransDate")]
        public DateTime TransDate { get; set; }

        [Display(Name = "DeliveredDate")]
        public String DeliveredDate { get; set; }

        [Display(Name = "StatusDesc")]
        public String StatusDesc { get; set; }

        [Display(Name = "Remark")]
        public String Remark { get; set; } 
    }
}
