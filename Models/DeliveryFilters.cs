﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class DeliveryFilters
    {


        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        [Display(Name = "Contacts")]
        public string ContactNo { get; set; }


        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}