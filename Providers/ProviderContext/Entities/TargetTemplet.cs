using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class TargetTemplet
    {
        [Column("target_id", Order = 1)]
        public int target_id { get; set; }
        [Column("description", Order = 2)]
        public string description { get; set; }
    }
}