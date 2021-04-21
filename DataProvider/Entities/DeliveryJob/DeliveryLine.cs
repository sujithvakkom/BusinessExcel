using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities.DeliveryJob
{
    public class DeliveryLine : DeliverySchedule
    {

        [Display(Name = "LineId")]
        public Int32 LineId { get; set; }

        [Display(Name = "SaleDate")]
        public DateTime? SaleDate { get; set; }

        [Display(Name = "LineNumber")]
        public Decimal LineNumber { get; set; }

        [Display(Name = "ItemCode")]
        public String ItemCode { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "OrderQuantity")]
        public Decimal OrderQuantity { get; set; }

        [Display(Name = "SelleingPrice")]
        public Decimal SelleingPrice { get; set; }
    }
}
