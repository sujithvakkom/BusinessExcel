using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public partial class ItemDetails
    {
        public int inventory_item_id { get; set; }
        public string item_code { get; set; }
        public string description { get; set; }
        public Nullable<decimal> price { get; set; }
        public string model { get; set; }
        public string model_description { get; set; }
    }
}