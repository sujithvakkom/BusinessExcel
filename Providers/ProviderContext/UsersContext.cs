using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity;
using System.Linq;

namespace BusinessExcel.Providers.ProviderContext
{
    public partial class UsersContext : DbContext
    {
        public UsersContext() 
            : base("BusinessExcelData")
        {
        }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public UserProfile GetUserProfile(string userName)
        {
            if (userName == null) return null;
            var result = UserProfile;
            var users = (from x in result.ToList()
                     where  x.UserName == userName
                         select x).ToList();
            return users[0];
        }
    }
}