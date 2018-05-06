using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BusinessExcel.Models
{
    public class RosterModel
    {

        [Required]    
        public Int32? RosterId { get; set; }


        [Required]
        [Display(Name = "User")]
        public Int32? UserId { get; set; }

 
        [Required]
        [Display(Name = "Location")]
        public Int32? LocationId { get; set; }


        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
    }
}