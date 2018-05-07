using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class BrandDetail
    {
        [Column("brand_id", Order = 1)]
        public int brand_id { get; set; }
        [Column("description", Order = 2)]
        public string description { get; set; }
    }
}