using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class ViewLocationTargetDistribution
    {
        public string UserName { get; set; }
        public string FullName   { get; set; }
        public string   Location  { get; set; }
        public string Period   { get; set; }
        public string Catogery  { get; set; }
        public decimal? Target   { get; set; }
        public decimal AllocatedPercentage { get; set; }

    }
}