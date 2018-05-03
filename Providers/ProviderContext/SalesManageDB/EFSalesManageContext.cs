using DBSalesManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.SalesManageDB
{
    public class EFSalesManageContext : DBSalesmanageEntities
    {
        public EFSalesManageContext()
            : base("SalesManageData") { }
    }
}