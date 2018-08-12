using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class CompetitorViewModel
    {

        [Display(Name = "Title")]
        public string title { get; set; }

        public string message { get; set; }

        [Display(Name = "User Name")]
        public string user_name { get; set; }


        public string name { get; set; }


        public DateTime create_time { get; set; }


    }
}