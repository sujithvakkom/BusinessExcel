using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class _BaseTarget
    {
        [Column("TargetTemplate", Order = 1)]
        public int TargetTemplate { get; set; }

        [Column("UserName", Order = 2)]
        public string UserName { get; set; }

        [Column("Description", Order = 3)]
        public string Description { get; set; }

        [Column("Location", Order = 4)]
        public int? Location { get; set; }

        [Column("StartDate", Order = 5)]
        public DateTime StartDate { get; set; }

        [Column("EndDate", Order = 6)]
        public DateTime EndDate { get; set; }

        [Column("TotalTarget", Order = 7)]
        public decimal? TotalTarget { get; set; }


        [Column("BaseIncentive", Order = 7)]
        public decimal? BaseIncentive { get; set; }

    }
}