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

        public static string LOCATIONTARGET_TITLE = "Location Target";
        public static string CREATELOCATIONTARGET_TITLE = "Create Location Target";

        public static string LOCATIONTARGET = "LocationTarget";

        public static string _LOCATIONTARGETEDITCREATE = "_LocationTargetEditCreate";
        public static string _VIEWLOCATIONTARGET = "_ViewLocationTarget";

        [HttpGet]
        public ActionResult LocationTarget()
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
            var data = new BaseTarget();
            if (Request.IsAjaxRequest())
                return PartialView(LOCATIONTARGET,data);
            return View(data);
        }

        [HttpPost]
        public ActionResult LocationTarget(BaseTarget target, ICollection<LineTarget> lineTarget)
        {
            string Message = "";
            int result = -1;
            target.LineTargets =
                lineTarget.ToArray();
            bool IsModelValid = true;
            IsModelValid = IsModelValid & target.StartDate == null;
            IsModelValid = IsModelValid & target.EndDate == null;
            IsModelValid = IsModelValid & target.Location == null;
            IsModelValid = IsModelValid & (from x in lineTarget
                                           where x.Target != null
                                           select x).Count() > 0;
            if (IsModelValid)
            {
                try
                {
                    result = target.Save(out Message);
                    if (result != -1)
                        target = new BaseTarget();
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
            else { Message = "Insufficient data."; }
            ViewBag.Result = result;
            if (!string.IsNullOrEmpty(Message))
                ViewBag.Message = Message;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];

            return PartialView(_LOCATIONTARGETEDITCREATE, target);
        }

        public ActionResult _ViewLocationTarget() {
            return View();
        }
    }
}