using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class CheckinViewModel
    {
        
        public DateTime shift_date { get; set; }

        public int user_id { get; set; }

        [Display(Name = "Staff Name")]
        public string user_name { get; set; }

        public string fullname { get; set; }

        public DateTime checkin_time { get; set; }

        public decimal checkin_latitude { get; set; }

        public decimal checkin_longitude { get; set; }
        public string checkin_address { get; set; }

        public DateTime checkout_time { get; set; }

        public decimal checkout_latitude { get; set; }

        public decimal checkout_longitude { get; set; }
        public string checkout_address { get; set; }

        public int? breaks { get; set; }

        public int? break_span { get; set; }

        public int? row_count { get; set; }

    }
}