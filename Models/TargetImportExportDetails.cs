using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class TargetImportExportDetails
    {
        public string TargetDescription { get; set; }
        public string EmployCode { get; set; }
        public string EmployName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }
        public int LocationID { get; set; }
        public string LocationDescription { get; set; }
        public decimal TargetValue { get; set; }
        public decimal LineAchivementAcc { get; set; }
        public decimal BonusAchievementAcc { get; set; }
        public decimal MinTotalAchievement { get; set; }
        public decimal MinLineAchievement { get; set; }
        public decimal MinBonusAchievement { get; set; }
        public decimal IncentiveCap { get; set; }
    }
}