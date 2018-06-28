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

        public static string TARGETLINE = "TargetLine";
        public static string TARGETTEMPLATE_TITLE = "Target Template";
        public static string TARGETASSIGN_TITLE = "Target Assign";
        public static string TARGETTEMPLATELINE_TITLE = "Target Lines";
        public static string TARGETTEMPLATE = "TargetTemplate";
        public static string _TARGETTEMPLATECREATEBLOCK = "_TargetTemplateCreateBlock";
        public static string _TARGETASSIGNBLOCK = "_TargetAssignBlock";
        public static string TARGETTEMPLATECREATE = "TargetTemplateCreate";
        [HttpGet]
        public ActionResult TargetTemplate()
        {
            BaseTarget target = null;
            if (target == null)
            {
                target = new BaseTarget();
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
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            if (Request.IsAjaxRequest())
                return PartialView(TARGETTEMPLATE,target);
            return View(target);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TargetTemplateCreate(BaseTarget target,ICollection<LineTarget> lineTarget)
        {
            int result = -1;
            if (ModelState.IsValid)
            {
                target.LineTargets =
                    lineTarget.ToArray();
                try
                {
                    result = target.Save();
                    target = new BaseTarget();
                }
                catch (Exception ex) {
                    target.LineTargets = lineTarget.ToArray();
                }
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
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            if (Request.IsAjaxRequest())
                return PartialView(_TARGETTEMPLATECREATEBLOCK, target);
            return View(target);

        }

    }
}