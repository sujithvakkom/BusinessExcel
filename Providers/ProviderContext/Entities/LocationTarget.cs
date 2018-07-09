using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class LocationTarget
    {
        [Column("target_id", Order = 1)]
        public int target_id { get; set; }

        [Column("name", Order = 2)]
        public string name { get; set; }

        [Column("start_date", Order = 3)]
        public DateTime start_date { get; set; }

        [Column("end_date", Order = 4)]
        public DateTime end_date { get; set; }

        [Column("location_id", Order = 5)]
        public int location_id { get; set; }

        [Column("description", Order = 6)]
        public string description { get; set; }
    }


    [ComplexType()]
    public class LocationTargetLine
    {
        [Column("row_num", Order = 1)]
        public Int64 row_num { get; set; }

        [Column("category_id", Order = 2)]
        public int category_id { get; set; }

        [Column("has_bonus", Order = 3)]
        public bool has_bonus { get; set; }

        [Column("category", Order = 4)]
        public string category { get; set; }

        [Column("value", Order = 5)]
        public decimal value { get; set; }
    }
}