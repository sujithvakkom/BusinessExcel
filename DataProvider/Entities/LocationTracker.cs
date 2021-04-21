using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataProvider.Entities
{
    [Table("LOCATION_TRACKER")]
    public class LocationTracker
    {

        [Column("VehicleCode", Order = 1)]
        public String VehicleCode;
        [Column("Latitude", Order = 1)]
        public Double Latitude;
        [Column("Longitude", Order = 1)]
        public Double Longitude;
        [Column("Accuracy", Order = 1)]
        public float accuracy;
        [Column("CreatedTime", Order = 1)]
        public DateTime CreatedTime;
    }
}