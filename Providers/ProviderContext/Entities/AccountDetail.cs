using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class AccountDetail
    {
        [Column("id", Order = 1)]
        public int id { get; set; }
        [Column("description", Order = 2)]
        public string description { get; set; }
    }
}
