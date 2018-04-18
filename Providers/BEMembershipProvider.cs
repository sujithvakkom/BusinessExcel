using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel.Providers
{

    public sealed class BEMembershipProvider : SimpleMembershipProvider
    {
        
        public override bool ValidateUser(string username, string password)
        {
            return base.ValidateUser(username, password);
        }

        public override string GetUserNameFromId(int userId)
        {
            return base.GetUserNameFromId(userId);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection collection = new MembershipUserCollection();
            using (var db = new UsersContext())
            {
                var users =
                db.UserProfiles;
                foreach (var x in users)
                {
                    collection.Add(new MembershipUser(this.Name, x.UserFullName, null, x.UserName,
                        null, null, false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue));
                }
            }
            totalRecords = collection.Count;
            return collection;
        }
        public DbSet<UserProfile> GetAllUsers()
        {
            using (var db = new UsersContext())
            {
                return db.UserProfiles;
            }
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return base.GetUser(username, userIsOnline);
        }
    }
}