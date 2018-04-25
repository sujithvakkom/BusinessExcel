using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Security;
using WebMatrix.WebData;
using System.Collections.Specialized;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Providers.ProviderContext;

namespace BusinessExcel.Providers
{

    public sealed class BEMembershipProvider : SimpleMembershipProvider
    {
        public override void Initialize(string name, NameValueCollection config)
        {
            using (UsersContext db = new UsersContext())
                db.UserProfile.Create();
            base.Initialize(name, config);

        }

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
                db.UserProfile;
                foreach (var x in users)
                {
                    collection.Add(new MembershipUser(this.Name, x.UserName, null, x.UserName,
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
                return db.UserProfile;
            }
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return base.GetUser(username, userIsOnline);
        }


        public static UserProfile GetUser(string username) {
            using (var db = new UsersContext())
                return db.GetUserProfile(username);
        }
    }
}