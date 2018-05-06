using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessExcel.Models
{
    [Table(name:"Rosters")]
    public class RosterModel
    {

        [Required]
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 roster_id { get; set; }


        [Required]
        [Key, Column(Order = 1)]
        [Display(Name = "User")]
        public Int32? user_id { get; set; }

 
        [Required]
        [Key, Column(Order = 2)]
        [Display(Name = "Location")]
        public Int32? location_id { get; set; }


        [Required]
        [Key, Column(Order = 3)]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? start_date { get; set; }

        [Required]
        [Key, Column(Order = 4)]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? end_date { get; set; }
    }
}