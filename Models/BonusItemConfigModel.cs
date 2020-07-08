using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BusinessExcel.Models
{
    [ComplexType()]
    public class BonusSettings
    {

        [Column("id")]
        public Int32 ID { get; set; }

        [Column("item_code")]
        public string item_code { get; set; }

        [Column("description")]
        public string description { get; set; }

        [Column("incentive")]
        public decimal incentive { get; set; }

    }

    public class BonusItemConfigModel
    {
        public BonusItemConfigModel() {
            Item = new ItemModel();
        }

        public ItemModel Item { get; set; }

        [Column("item_code")]
        [Display(Name = "ItemCode")]
        public string ItemCode { get; set; }

        [Column("description")]
        [Display(Name = "ItemName")]
        public string ItemName { get; set; }

        [Column("incentive")]
        [Display(Name = "Incentive Amount", Prompt = "Incentive Amount", Description = "Incentive Amount", ShortName = "Incentive Amount")]
        public decimal IncentiveAmount { get; set; }

        [Display(Name = "category_id")]
        public int category_id { get; set; }

        public int WeekId { get; set; }
        public BonusWeek Week { get {
                return new BonusWeek() { Id = WeekId };
            } }

        public int InventoryItemId { get; internal set; }
    }

    [ComplexType()]
    public class BonusAchivement {
        public string item_code { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public Decimal sale { get; set; }
        public Decimal bonus { get; set; }
    }

    public class BonusStaffView
    {
        public UserDetail User { get;  set; }
        public UserTargetDetailsView Target { get; internal set; }
        public List<BonusAchivement> Bonus { get; internal set; }
        public IEnumerable<BonusWeek> Weeks { get; internal set; }

        public decimal GetBonusTotal()
        {
            return Bonus.Sum(m => m.bonus);
        }

        [Display(Name ="Category")]
        public int? CategoyID { get; set; }
        public string StaffCode { get; internal set; }
    }
}