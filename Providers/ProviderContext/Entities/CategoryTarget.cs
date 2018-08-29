using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public partial class CategoryTarget
    {
        //user_name	display_name	target_description	date	category_description	value	has_bonus
        

        [Column("user_name", Order = 1)]
        public string user_name { get; set; }

        [Column("display_name", Order = 2)]
        public string display_name { get; set; }

        [Column("target_description", Order = 3)]
        public string target_description { get; set; }

        [Column("date", Order = 5)]
        public string date { get; set; }

        [Column("category_description", Order = 5)]
        public string category_description { get; set; }

        [Column("value", Order = 6)]
        public decimal value { get; set; }

        [Column("has_bonus", Order = 7)]
        public bool has_bonus { get; set; }

        [Column("category_id", Order = 8)]
        public int category_id { get; set; }
    }
}