using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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