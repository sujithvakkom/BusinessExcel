using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);

        }


        internal static bool CheckTableExists(DbContext db, string tableName)
        {
            var query = db.Database.SqlQuery<object>(
                @"SELECT * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = @TABLE_NAME",
                new System.Data.SqlClient.SqlParameter { ParameterName = "TABLE_NAME", Value = tableName });
            try
            {
                var output = query.ToList();
                return output.Count > 0;
            }
            catch (Exception)
            { return false; }
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
                            context.Database.Create();
                        if (!CheckTableExists(context, "UserProfile"))
                            context.UserProfile.Create();
                    }

                    if (!WebSecurity.Initialized)
                        WebSecurity.InitializeDatabaseConnection("BusinessExcelData", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    if (!WebSecurity.UserExists("srkrishnan@grandstores.ae"))
                    {
                        WebSecurity.CreateUserAndAccount("srkrishnan@grandstores.ae", "pass");

                        using (var db = new UsersContext())
                        { 
                            UserProfile user = db.UserProfile.SingleOrDefault(x => x.UserName == "srkrishnan@grandstores.ae");
                            if (user == null) user = new UserProfile() { UserName = "srkrishnan@grandstores.ae", UserFullName = "Sujith Radhakrishnan" };
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