using System.Data.Entity;

namespace BusinessExcel.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext() 
            : base("BusinessExcelData")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}