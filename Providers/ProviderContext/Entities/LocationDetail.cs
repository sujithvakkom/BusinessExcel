using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class LocationDetail
    {
        [Column("location_id", Order = 1)]
        public int location_id { get; set; }
        [Column("description", Order = 2)]
        public string description { get; set; }
        [Column("account", Order = 3)]
        public string account { get; set; }
    }
}
