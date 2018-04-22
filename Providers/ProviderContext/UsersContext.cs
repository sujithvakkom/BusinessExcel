using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity;

namespace BusinessExcel.Providers.ProviderContext
{
    public class UsersContext : DbContext
    {
        public UsersContext() 
            : base("BusinessExcelData")
        {
        }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}