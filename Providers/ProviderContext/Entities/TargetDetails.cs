using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class TargetDetails
    {
        [Column("target_id", Order = 1)]
        public int target_id { get; set; }

        [Column("description", Order = 2)]
        public string description { get; set; }

        //[Column("incentive_pct", Order = 2)]
        //public string incentive_pct { get; set; }
    }
}
