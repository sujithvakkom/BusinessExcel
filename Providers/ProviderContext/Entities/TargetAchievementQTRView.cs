using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    //daily_update_v
  //  [Table(name: "user_target_achievement_v", Schema = "db_salesmanage_user")]
    public partial class TargetAchievementQTRView
    {


        //[Column("create_time")]
        //[DataType(DataType.Date)]
        //public DateTime? CreateTime { get; set; }

        //[Column("name")]
        //public string Name { get; set; }

        //[Column("location")]
        //public string Location { get; set; }

        //[Key()]
        //[Column("user_name")]
        //public string UserName { get; set; }



        //[Column("location_id")]
        //public string location_id { get; set; }



        //// [Key, Column(Order = 3)]
        //[Display(Name = "From Date")]
        //[DataType(DataType.Date)]
        //public DateTime? fromdate { get; set; }


        ////  [Key, Column(Order = 4)]
        //[Display(Name = "To Date")]
        //[DataType(DataType.Date)]
        //public DateTime? todate { get; set; }

        [Key()]
        [Column("user_id")]
        public int? user_id { get; set; }

        [Column("Category")]
        public string Category { get; set; }



        // [Key, Column(Order = 1)]
        [Column("Target_Amt")]
        [Display(Name = "Target")]
        public decimal? Target_amt { get; set; }


        [Column("Achieved_Amt")]
        [Display(Name = "Achieved")]
        public decimal? Achieved_Amt { get; set; }


        [Column("Entered_Amt")]
        [Display(Name = "Entered")]
        public decimal? Entered_Amt { get; set; }


        [Column("has_bonus")]
        [Display(Name = "Bonus Line")]
        public bool has_bonus { get; set; }

        [NotMapped]
        [Display(Name = "Month")]
        public string Month { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? start_date { get; set; }


        //  [Key, Column(Order = 4)]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? end_date { get; set; }

        //[Column("Base_Incentive")]
        //[Display(Name = "Base Incentive")]
        //public decimal? Base_Incentive { get; set; }


       
        public string qtr { get; set; }


        [Column("category_id")]
        public int? category_id { get; set; }

  
        [Column("target_id")]
        public int? target_id { get; set; }


        public List<TargetTotalView> TargetTotal { get; set; }
        public List<UserTargets> UserTargets { get; set; }
        //[Column("Target_Per")]
        //[Display(Name = "Target %")]
        //public string Target_Per { get; set; }


        //[Column("Line_Achiv")]
        //[Display(Name = "Line Achiv")]
        //public string Line_Achiv { get; set; }


        //[Column("Bonus_Lines")]
        //[Display(Name = "Bonus Lines")]
        //public string Bonus_Lines { get; set; }

        // public TargetMaster Target { get; set; }

    }
}