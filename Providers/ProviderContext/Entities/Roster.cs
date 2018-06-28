using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;



namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [Table(name: "Roster",Schema = "db_salesmanage_user")]
    public class Roster
    {

        [Required]
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 roster_id { get; set; }


       // [Required]
       //// [Key, Column(Order = 1)]
       // [Display(Name = "User")]
       // public string user_name { get; set; }

       // //[Required]
       // // [Key, Column(Order = 1)]
       // [Display(Name = "User ID")]
       // public int user_id { get; set; }



        [Required]
       // [Key, Column(Order = 2)]
        [Display(Name = "Location")]
        public string location_id { get; set; }


        [Required]
       // [Key, Column(Order = 3)]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? start_date { get; set; }

        [Required]
      //  [Key, Column(Order = 4)]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? end_date { get; set; }


       
        //// [Key, Column(Order = 1)]
        //[Display(Name = "Roster Target Amt")]
        //public string target_amt { get; set; }



        //public string u_name { get; set; }



       
        public string name { get; set; }


        //[Required]
        //// [Key, Column(Order = 1)]
        //[Display(Name = "Target")]
        //public Int32? target_id { get; set; }


    }
}
