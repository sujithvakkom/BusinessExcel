using System.ComponentModel.DataAnnotations.Schema;

namespace DataProvider.Entities
{
    [ComplexType()]
    public class UserDetail
    {
        [Column("USER_NAME", Order = 1)]
        public string user_name { get; set; }
        [Column("FULL_NAME", Order = 2)]
        public string full_name { get; set; }
        [Column("VEHICLE_CODE", Order = 2)]
        public string vehicle_code { get; set; }
        [Column("PROFILE", Order = 3)]
        public string profile { get; set; }
        [Column("SIGNATURE", Order = 3)]
        public string signature { get; set; }
        [Column("SESSION", Order = 3)]
        public string session { get; set; }



    }
}
