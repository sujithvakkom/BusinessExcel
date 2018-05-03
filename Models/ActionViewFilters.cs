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
    }
}