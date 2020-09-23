using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    /* inv.inventory_item_id ,
       inv.description ,
       inv.model_id ,
       inv.item_code ,
       model.description AS model_description ,
       model.model,
	   ISNULL(price.price,0) price
                */

    [ComplexType()]
    public partial class ItemModel
    {
        [Display(Name = "Item", Prompt = "Item", Description = "Item", ShortName = "Item")]
        [Column("inventory_item_id", Order = 1)]
        public int inventory_item_id { get; set; }

        [Column("model_id", Order = 2)]
        public int model_id { get; set; }

        [Display(Name = "Item Code", Prompt = "Item Code", Description = "Item Code", ShortName = "Item Code")]
        [Column("item_code", Order = 3)]
        public string item_code { get; set; }

        [Display(Name = "description", Prompt = "description", Description = "description", ShortName = "description")]
        [Column("description", Order = 4)]
        public string description { get; set; }

        [Column("model_description", Order = 7)]
        public string model_description { get; set; }

        [Column("model", Order = 6)]
        public string model { get; set; }

        [Column("price", Order = 5)]
        public Nullable<decimal> price { get; set; }
    }
}