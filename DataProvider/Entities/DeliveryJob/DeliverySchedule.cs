using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities.DeliveryJob
{
    public class DeliverySchedule
    {
        [Display(Name = "Status")]
        public Int32 Status { get; set; }

        [Display(Name = "ScheduledDate")]
        public DateTime? ScheduledDate { get; set; }

        [Display(Name = "TimeSlot")]
        public String TimeSlot { get; set; }

        [Display(Name = "DriverName")]
        public String DriverName { get; set; }

        [Display(Name = "VehicleCode")]
        public String VehicleCode { get; set; }

        [Display(Name = "GPS")]
        public String GPS { get; set; }

        [Display(Name = "MapURL")]
        public String MapURL { get; set; }

        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        [Display(Name = "Emirate")]
        public String Emirate { get; set; }

        [Display(Name = "UserName")]
        public String UserName { get; set; }
    }
}
