using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public partial class ActionViewFilters
    {



        [Display(Name = "User")]
        public string UserID { get; set; }


        [Display(Name = "Location")]
        public string LocationID { get; set; }


        [Display(Name = "Target")]
        public string Target { get; set; }


    }
}