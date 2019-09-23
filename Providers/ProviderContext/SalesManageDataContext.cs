
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public SalesManageDataContext()
            : base("SalesManageData")
        {
            Database.SetInitializer<SalesManageDataContext>(null);
        }
        public SalesManageDataContext(String ConnectionString)
            : base(ConnectionString)
        {
            Database.SetInitializer<SalesManageDataContext>(null);
        }

        public DbSet<DailyUpateView> DailyUpateView { get; set; }

        //public DbSet<ItemDetails> ItemDetails { get; set; }
    }
}
