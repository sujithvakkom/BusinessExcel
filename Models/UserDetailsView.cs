using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class UserTargetDetailsView
    {
        [Key]
        [Column("user_id")]
        [Display(Name = "Name")]
        public int UserID { get; set; }

        [Column("user_name")]
        [Display(Name = "Staff Code")]
        public string UserName { get; set; }
 


        [Column("full_name")]
        [Display(Name = "Full_Name")]
        public string Full_Name { get; set; }


        [Column("Location_Id")]
        [Display(Name = "Site")]
        public int Location_Id { get; set; }



        [Column("spr_vsr_id")]
        public int SuperVisorId { get; set; }


   
        [Display(Name = "FieldMan")]
        public string FieldMan { get; set; }


        [Display(Name = "SalesMan")]
        public string SalesMan { get; set; }



        [Display(Name = "Location_Name")]
        public string Location_Name { get; set; }


        
        [Display(Name = "Period")]
        public string Quarter_Name { get; set; }


        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? start_date { get; set; }

  
        //  [Key, Column(Order = 4)]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? end_date { get; set; }



        [Display(Name = "Target Type")]
        public string Type { get; set; }


        [Display(Name = "Account")]
        public string Account { get; set; }

        public List<string> QuarterMonths { get; set; }

    }
}