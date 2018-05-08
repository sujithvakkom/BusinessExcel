using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class RosterContext: DbContext
    {
        public RosterContext() 
            : base("BusinessExcelData")
        {
           //Database.SetInitializer<RosterContext>(null);
        }


        public DbSet<RosterModel> UserRoster { get; set; }
        public List<RosterModel> GetAllRosters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<RosterContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}