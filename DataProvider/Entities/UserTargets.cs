using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataProvider.Entities
{
    public class UserTargets
    {
        [Column("target_id")]
        public int target_id { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? start_date { get; set; }

        //  [Key, Column(Order = 4)]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? end_date { get; set; }

        public string Month { get; set; }


        public List<TargetAchievementView> TargetDetailsView { get; set; }
        public List<TargetTotalView> TargetTotalView { get; set; }
    }
}