using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("BusinessExcelData", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    if (!WebSecurity.UserExists("srkrishnan@grandstores.ae"))
                    {
                        WebSecurity.CreateUserAndAccount("srkrishnan@grandstores.ae", "pass");

                        using (var db = new UsersContext())
                        {
                            UserProfile user = db.UserProfiles.SingleOrDefault(x => x.UserName == "srkrishnan@grandstores.ae");
                            user.UserFullName = "Sujith Radhakrishnan";
                            db.SaveChanges();
                        }
                    }

                    if (!Roles.RoleExists("System Administrator"))
                        Roles.CreateRole("System Administrator");

                    if (!Roles.IsUserInRole("srkrishnan@grandstores.ae", "System Administrator"))
                        System.Web.Security.Roles.AddUserToRole("srkrishnan@grandstores.ae", "System Administrator");

                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}