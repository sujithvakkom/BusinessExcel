using BootstrapHtmlHelper;
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
        public static string TARGETDISTRIBUTION = "TargetDistribution";
        public static string _TARGETDISTRIBUTIONASSIGN = "_TargetDistributionAssign";
        public static string TARGETDISTRIBUTION_TITLE = "Target Distribution";

        public static string _GETLOCATIONALOCATION = "_GetLocationAlocation";
        public ActionResult TargetDistribution()
        {
            BaseTarget target = new BaseTarget(true);
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
                return PartialView(TARGETDISTRIBUTION, target);
            return View(target);
        }

        [HttpPost]
        public ActionResult _GetLocationAlocation(BaseTarget target)
        {
            List<LineTarget> lineTargets = new List<LineTarget>();
            using (var db = new SalesManageDataContext())
            {
                try
                {
                    lineTargets = db.getTargetTempletLineDetails(int.Parse(target.TargetTemplate));
                }
                catch (Exception) { }
            }
            target.LineTargets = lineTargets.ToArray();
            return PartialView(target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TargetDistribution(BaseTarget target)
        {
            string Message = "";
            int result = -1;
            if (ModelState.IsValid)
            {
                try
                {
                    result = target.Save(out Message);
                    if (result != -1)
                        target = new BaseTarget(true);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Message;
                }
            }
            else
            {
                ViewBag.ModelErrors = ViewData.ModelState.GetErrors();
            }

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
            ViewBag.Result = result;
            if (!string.IsNullOrEmpty(Message))
                ViewBag.Message = Message;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            if (Request.IsAjaxRequest())
                return PartialView(_TARGETDISTRIBUTIONASSIGN, target);
            return View(target);

        }
    }
}