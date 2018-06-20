using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class CategoryDetail
    {
        [Column("category_id", Order = 1)]
        public int category_id { get; set; }

        [Column("description", Order = 2)]
        public string description { get; set; }
    }
}