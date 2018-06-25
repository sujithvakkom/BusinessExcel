using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{ 
//    target_id	int	Unchecked
//    roster_id	int	Checked
//    user_id	int	Checked
//    description	nvarchar(50)	Unchecked
//    base_incentive	decimal(18, 2)	Unchecked
//    line_achieve_acc	decimal(18, 2)	Unchecked
//    bonus_achieve_acc	decimal(18, 2)	Unchecked
//    min_total_achievement	decimal(18, 2)	Unchecked
//    min_line_achievement	decimal(18, 2)	Unchecked
//    min_bonus_achievement	decimal(18, 2)	Unchecked
//    cap_base_incentive	decimal(18, 2)	Checked

    [Table(name: "target_m", Schema = "db_salesmanage_user")]
    public class TargetMaster
    {
        //target_id	int	Unchecked
        [Required]
        [Key, Column(name: "target_id", Order = 0), 
            DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Target Id")]
        public int TargetID { get; set; }

        //roster_id	int	Checked
        [Required]
        [Column(name: "roster_id", Order = 1)]
        [Display(Name = "Roster ID")]
        public int? RosterID { get; set; }

        //user_id	int	Checked
        [Required]
        [Column(name: "user_id", Order = 2)]
        [Display(Name = "User ID")]
        public int? UserID { get; set; }

        //description	nvarchar(50)	Unchecked
        [Display(Name = "Target Name")]
        [Column(name: "description", Order = 3)]
        public string Description { get; set; }

        // base_incentive	decimal(18, 2)	Unchecked
        [Display(Name = "Base Incentive")]
        [Column(name: "base_incentive", Order = 4)]
        public decimal BaseIncentive { get; set; }

        //    line_achieve_acc	decimal(18, 2)	Unchecked
        [Display(Name = "Line Achievement Acc. (%)")]
        [Column(name: "line_achieve_acc", Order = 5)]
        public decimal LineAchieveAcc { get; set; }

        //    bonus_achieve_acc	decimal(18, 2)	Unchecked
        [Display(Name = "Bonus Achievement Acc. (%)")]
        [Column(name: "bonus_achieve_acc", Order = 6)]
        public decimal BonusAchieveAcc { get; set; }

        //    min_total_achievement	decimal(18, 2)	Unchecked
        [Display(Name = "Min Total Achievement (%)")]
        [Column(name: "min_total_achievement", Order = 7)]
        public decimal MinTotalAchievement { get; set; }

        //    min_line_achievement	decimal(18, 2)	Unchecked
        [Display(Name = "Min Line Achievement (%)")]
        [Column(name: "min_line_achievement", Order = 8)]
        public decimal MinLineAchievement { get; set; }

        //    min_bonus_achievement	decimal(18, 2)	Unchecked
        [Display(Name = "Min Bonus Achievement (%)")]
        [Column(name: "min_bonus_achievement", Order = 9)]
        public decimal MinBonusAchievement { get; set; }

        //    cap_base_incentive	decimal(18, 2)	Checked
        [Display(Name = "Base Incentive Cap")]
        [Column(name: "cap_base_incentive", Order = 10)]
        public decimal CapBaseIncentive { get; set; }

        private List<TargetDetail> _TargetDetails;
        public virtual List<TargetDetail> TargetDetails
        {
            get
            {
                using (var db = new SalesManageDataContext())
                {
                    var _TargetDetails = db.TargetDetails
                           .Where(x => x.TargetID == this.TargetID && x.UserID.HasValue)
                           .Select(x => x).ToList();

                    foreach (var x in _TargetDetails)
                    {

                        x.TargetID = x.TargetID;
                        x.TargetLineID = x.TargetLineID;
                        x.Achievement = x.Achievement.HasValue ? x.Achievement : 0;
                        x.CategoryID = x.CategoryID.HasValue ? x.CategoryID : 0;
                        x.HasBonus = x.HasBonus;
                        x.IncentivePct = x.IncentivePct.HasValue ? x.IncentivePct : 0;
                        x.ModelID = x.ModelID.HasValue ? x.ModelID : 0;
                        x.RosterId = x.RosterId.HasValue ? x.RosterId : 0;
                        x.TargetQty = x.RosterId.HasValue ? x.RosterId : 0;
                        x.UserID = x.UserID.HasValue ? x.UserID : 0;
                        x.Value = x.Value.HasValue ? x.Value : 0;
                    }

                }
                return _TargetDetails;
            }
            private set { _TargetDetails = value; }
        }

    }
}