using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class UserLocation
    {
        public int? UserID { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? Type { get; set; }
    }
}