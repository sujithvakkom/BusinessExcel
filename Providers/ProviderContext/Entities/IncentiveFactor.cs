using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class IncentiveFactor
    {
        [Column("account", Order = 1)]
        public string account { get; set; }

        [Column("category", Order = 2)]
        public string category { get; set; }

        [Column("factor", Order = 3)]
        public decimal factor { get; set; }

        [Column("threshold", Order = 3)]
        public decimal threshold { get; set; }
    }
}