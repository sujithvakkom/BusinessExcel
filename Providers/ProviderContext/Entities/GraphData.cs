using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class GraphData
    {
        public string User { get; set; }
        public string UserName { get; set; }
        public string Item { get; set; }
        public string ItemDescription { get; set; }
        public Decimal TotalValue { get; set; }
    }
}