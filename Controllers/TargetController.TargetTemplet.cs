using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel.Controllers
{
    public partial class TargetController : Controller
    {

        public static string TARGETTEMPLATE_TITLE = "Target Template";
        public static string TARGETTEMPLATE = "TargetTemplate";
        public ActionResult TargetTemplate(string sort, string sortdir, int page = 1, TargetViewFilters Filters = null)
        {
            if (!Roles.RoleExists("System Administrator")) Roles.CreateRole("System Administrator");
            if (!Roles.GetRolesForUser().Contains("System Administrator") && WebSecurity.CurrentUserName == "sujithvakkom@gmail.com")
                Roles.AddUserToRole(WebSecurity.CurrentUserName, "System Administrator");

            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + TARGETTEMPLATE;
            using (var db = new UsersContext())
            {
                try
                {
                    Session[Index.USER_PROFILE_INDEX] = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;
                }
                catch (Exception)
                {
                    WebSecurity.Logout();
                    RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
                }
            }
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            if (Request.IsAjaxRequest())
                return PartialView(TARGETTEMPLATE);

            return View();

        }
    }
}