using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class CheckinViewModel
    {
        
        public int user_id { get; set; }
        public string checktype { get; set; }

        [Display(Name = "Staff Name")]
        public string user_name { get; set; }
        public string fullname { get; set; }
        public int type { get; set; }
        public DateTime checkin_time { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }

    }
}