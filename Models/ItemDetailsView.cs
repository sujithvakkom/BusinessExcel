using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class ItemDetailsView
    {
        [Column("inventory_item_id", Order = 1)]
        public int inventory_item_id { get; set; }


        [Display(Name = "Code")]
        [Column("item_code", Order = 2)]
        public string item_code { get; set; }
        [Column("description", Order = 3)]
        public string description { get; set; }
        [Column("price", Order = 4)]
        public Nullable<decimal> price { get; set; }
        [Column("model", Order = 5)]
        public string model { get; set; }
        [Column("model_description", Order = 6)]
        public string model_description { get; set; }

        [Column("category_id", Order = 7)]
        public int category_id { get; set; }

        [Column("category_description", Order = 8)]
        public string category_description { get; set; }
    }
}