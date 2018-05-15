
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

      
        public DbSet<Roster> Roster { get; set; }

    }
}
