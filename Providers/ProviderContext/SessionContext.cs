using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext
{
    public class SessionContext : DbContext
    {
        public SessionContext(String nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<Session> Sessions { get; set; }
    }
}