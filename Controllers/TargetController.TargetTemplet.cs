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
        public static string TARGETTEMPLATE_TITLE = "Target Creation";
        public static string TARGETASSIGN_TITLE = "Target Assign";
        public static string TARGETTEMPLATELINE_TITLE = "Target Lines";
        public static string TARGETTEMPLATE = "TargetTemplate";
        public static string _TARGETASSIGNBLOCK = "_TargetAssignBlock";
        public static string TARGETTEMPLATECREATE = "TargetTemplateCreate";
        public static string _TEMPLETLINES = "_TempletLines";
        [HttpGet]
        public ActionResult TargetTemplate()
        {
            BaseTarget target = null;
            if (target == null)
            {
                target = new BaseTarget(true);
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
                return PartialView(TARGETTEMPLATE, target);
            return View(target);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TargetTemplateCreate(BaseTarget target, ICollection<LineTarget> lineTarget)
        {
            string Message="";
            int result = -1;
            if (ModelState.IsValid)
            {
                target.LineTargets =
                    lineTarget.ToArray();
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
            else {
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
                return PartialView(_TARGETTEMPLATECREATEBLOCK, target);
            return View(target);

        }

        public PartialViewResult _TempletLines(int Target)
        {
            using (var db = new SalesManageDataContext())
            {
                var LineTargets = db.getTargetTempletLineDetails(Target).ToArray();
                return PartialView(TARGETLINE, LineTargets);
            }
        }

        public static string _TARGETTEMPLATECREATEBLOCK = "_TargetTemplateCreateBlock";
        public PartialViewResult _TargetTemplateCreateBlock() { return PartialView(); }
        public static string TARGETLINE = "TargetLine";
        public PartialViewResult TargetLine() { return PartialView(); }
    }
}