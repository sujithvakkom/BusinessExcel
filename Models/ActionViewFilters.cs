using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class ActionViewFilters
    {

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }
        [Display(Name ="Brand")]
        public string BrandID { get; set; }

        [Display(Name ="Location")]
        public string Location { get; set; }


    }
}