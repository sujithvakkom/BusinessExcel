using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public partial class ActionViewFilters
    {

      

        [Display(Name = "UserID")]
        public string UserID { get; set; }


        [Display(Name ="LocationID")]
        public string LocationID { get; set; }

   


    }
}