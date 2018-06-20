using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public partial class CategoryAchievement
    {
        //user_id user_name   display_name target_id   category_id description sum_value target_value    achievent_pct

        [Column("user_id", Order = 1)]
        public int user_id { get; set; }

        [Column("user_name", Order = 2)]
        public string user_name { get; set; }

        [Column("display_name", Order = 3)]
        public string display_name { get; set; }

        [Column("target_id", Order = 4)]
        public int target_id { get; set; }

        [Column("category_id", Order = 5)]
        public int category_id { get; set; }

        [Column("description", Order = 6)]
        public string description { get; set; }

        [Column("sum_value", Order = 7)]
        public decimal sum_value { get; set; }

        [Column("target_value", Order = 8)]
        public decimal target_value { get; set; }

        [Column("achievent_pct", Order = 9)]
        public decimal achievent_pct { get; set; }
    }
}