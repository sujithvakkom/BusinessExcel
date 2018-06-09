using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    //daily_update_v
    [Table(name: "target_achievement", Schema = "db_salesmanage_user")]
    public partial class TargetAchievementView
    {
        
      
        //[Column("create_time")]
        //[DataType(DataType.Date)]
        //public DateTime? CreateTime { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Key()]
        [Column("user_name")]
        public string UserName { get; set; }



        //[Column("location_id")]
        //public string location_id { get; set; }


        
        // [Key, Column(Order = 3)]
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime? fromdate { get; set; }

        
        //  [Key, Column(Order = 4)]
        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime? todate { get; set; }



        // [Key, Column(Order = 1)]
        [Column("Target_Amt")]
        [Display(Name = "Target")]
        public string target_amt { get; set; }


        [Column("Achieved_Amt")]
        [Display(Name = "Achieved")]
        public string achieved_amt { get; set; }
    }
}