using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    //daily_update_v
    [Table(name:"daily_update_v", Schema = "sc_salesmanage_merchant")]
    public partial class DailyUpateView
    {
        [Key]
        [Column("ROW_NUM")]
        [Display(Name = "ROW NUM", Description = "ROW NUM", Prompt = "ROW NUM", ShortName = "R NUM")]
        public Int64 ROW_NUM { get; set; }
        [Column("user_id")]
        [Display(Name = "User Id", Description = "User Id", Prompt = "User Id", ShortName = "U Id")]
        public int UserId { get; set; }
        [Column("brand_id")]
        public int BrandId { get; set; }
        [Column("category_id")]
        public int? CategoryId { get; set; }
        [Column("model_id")]
        public int ModelId { get; set; }
        [Column("create_time")]
        public Nullable<System.DateTime> CreateTime { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("brand")]
        public string Brand { get; set; }
        [Column("category")]
        public string Category { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("item")]
        public string Item { get; set; }
        [Column("quantity")]
        public Nullable<int> Quantity { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("value")]
        public Nullable<decimal> TotalValue { get; set; }
    }
}