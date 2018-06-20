
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public DbSet<TargetMaster> TargetMaster { get; set; }
        // public DbSet<Roster> RosterList { get; set; }

        //public DbSet<ItemDetails> ItemDetails { get; set; }

            
    }
}
