using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities.DeliveryJob
{
    public class DeliveryNotification
    {
        [Display(Name = "Id")]
        public Int32 Id { get; set; }

        [Display(Name = "HeaderId")]
        public Int32 HeaderId { get; set; }

        [Display(Name = "OrderNumber")]
        public String OrderNumber { get; set; }

        [Display(Name = "PhoneNumber")]
        public String PhoneNumber { get; set; }

        [Display(Name = "Message")]
        public String Message { get; set; }

        [Display(Name = "ErrorMessage")]
        public String ErrorMessage { get; set; }

        [Display(Name = "CreatedOn")]
        public DateTime CratedOn { get; set; }

        [Display(Name = "Status")]
        public Int32 Status { get; set; }

        [Display(Name = "UserName")]
        public String UserName { get; set; }
    }
}
