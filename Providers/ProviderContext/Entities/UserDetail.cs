using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class UserDetail
    {
        [Column("user_name", Order = 1)]
        public string user_name { get; set; }
        [Column("full_name", Order = 2)]
        public string full_name { get; set; }
    }
}