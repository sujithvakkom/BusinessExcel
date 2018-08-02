using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class UserLocation
    {
        [Column("user_name", Order = 1)]
        public int? UserID { get; set; }
        [Column("checkin_time", Order = 2)]
        public int? CheckinTime { get; set; }
        [Column("user_name", Order = 3)]
        public decimal? Latitude { get; set; }
        [Column("user_name", Order = 4)]
        public decimal? Longitude { get; set; }
        [Column("user_name", Order = 5)]
        public int? Type { get; set; }
    }
}