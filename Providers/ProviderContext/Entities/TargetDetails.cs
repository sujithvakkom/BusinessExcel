using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    //target_id int Unchecked
    //target_line_id int Unchecked
    //user_id int Checked
    //roster_id int Checked
    //category_id int Checked
    //model_id int Checked
    //value decimal(10, 2)	Checked
    //target_qty  int Checked
    //has_bonus bit Checked
    //achievement decimal(18, 2)	Checked
    //incentive_pct   decimal(3, 2)	Checked

    [Table(name: "user_target", Schema = "db_salesmanage_user")]
    public class TargetDetail
    {
        //target_id int Unchecked
        [Required]
        [Key, Column(name: "target_id", Order = 0)]
        [Display(Name = "Target Id")]
        //[ForeignKey("target_m")]
        public int TargetID { get; set; }

        //target_line_id int Unchecked
        [Required]
        [Key]
        [Column(name: "target_line_id", Order = 1)]
        [Display(Name = "Target Line Id")]
        //[ForeignKey("target_m")]
        public int TargetLineID { get; set; }

        //user_id int Checked
        [Key]
        [Column(name: "user_id", Order = 2)]
        [Display(Name = "User Id")]
        //[ForeignKey("target_m")]
        public int? UserID { get; set; }

        //roster_id int Checked
        [Display(Name = "Roster Id")]
        [Column(name: "roster_id", Order = 3)]
        public int? RosterId { get; set; }

        //category_id int Checked
        [Display(Name = "Category ID")]
        [Column(name: "category_id", Order = 5)]
        public int? CategoryID { get; set; }

        //model_id int Checked
        [Display(Name = "Model Id")]
        [Column(name: "model_id", Order = 6)]
        public int? ModelID { get; set; }

        //value decimal(10, 2)	Checked
        [Display(Name = "Value")]
        [Column(name: "value", Order = 7)]
        public decimal? Value { get; set; }

        //target_qty  int Checked
        [Display(Name = "Target Qty")]
        [Column(name: "target_qty", Order = 8)]
        public int? TargetQty { get; set; }

        //has_bonus bit Checked
        [Display(Name = "Has Bonus")]
        [Column(name: "has_bonus", Order = 9)]
        public bool HasBonus { get; set; }

        //achievement decimal(18, 2)	Checked
        [Display(Name = "Has Bonus")]
        [Column(name: "achievement", Order = 10)]
        public decimal? Achievement { get; set; }

        //incentive_pct   decimal(3, 2)	Checked
        [Display(Name = "Incentive (%)")]
        [Column(name: "incentive_pct", Order = 11)]
        public decimal? IncentivePct { get; set; }

    }
}