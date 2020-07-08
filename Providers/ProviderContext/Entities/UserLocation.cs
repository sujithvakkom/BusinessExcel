using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class UserLocation
    {
        public string UserName { get; set; }

        [Column("user_id", Order = 1)]
        private int? user_id { get; set; }
        [Column("checkin_time", Order = 2)]
        private DateTime? checkin_time { get; set; }
        [Column("latitude", Order = 3)]
        public decimal? Latitude { get; set; }
        [Column("longitude", Order = 4)]
        public decimal? Longitude { get; set; }
        [Column("type", Order = 5)]
        public int? Type { get; set; }

        [Column("address", Order = 6)]
        public string Address { get; set; }

        public int? UserID { get { return user_id; } set { user_id = value; } }
        public DateTime? CheckinTime { get { return checkin_time; } }
    }
}