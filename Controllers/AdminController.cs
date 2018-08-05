using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace BusinessExcel.Controllers
{
    [Authorize(Roles = BERoleDetails.SYSTEM_ADMINISTRATOR)]
    public partial class AdminController : Controller
    {
        public const string ADMIN = "Admin";

        public static string USERMANAGEMENT = "UserManagement";
        public static string USERMANAGEMENT_TITLE = "User Management";

        public static string VIEWMANAGEMENT = "ViewManagement";
        public static string VIEWMANAGER_TITLE = "View Management";
        public static string AJAXCREATEROLE = "AjaxCreateRole";
        public static string ROLETABLEPARTIAL = "RoleTablePartial";
        public static string ROLECREATIONMESSAGE = "RoleCreationMessage";
        public static string AJAXREMOVEROLE = "AjaxRemoveRole";
        //
        public static string USERREQUESTTABLEPARTIAL = "UserRequestTablePartial";
        public static string USERSTABLEPARTIAL = "UsersTablePartial";

        

        // GET: /Admin/UserManagement

        [Authorize(Roles = "System Administrator")]
        [HttpGet]
        public ActionResult UserManagement()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERMANAGEMENT_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = USERMANAGEMENT_TITLE;
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }

        [HttpGet]
        public ActionResult ViewManagement(UserProfile UserProfile)
        {
            using (UsersContext db = new UsersContext())
                UserProfile = db.GetUserProfile(UserProfile.UserName);
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + VIEWMANAGER_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = VIEWMANAGER_TITLE;
            if (Request.IsAjaxRequest())
                return PartialView();
            return View(UserProfile);
        }
        [HttpPost]
        [Authorize(Roles = "System Administrator")]
        public PartialViewResult AjaxCreateRole(RolesNameModel RoleName)
        {
            ViewData.Add(ROLECREATIONMESSAGE, "");
            if (ModelState.IsValid)
            {
                if (!Roles.RoleExists(RoleName.RolesName))
                {
                    Roles.CreateRole(RoleName.RolesName);
                    ViewData[ROLECREATIONMESSAGE] = "Role Created.";
                    RoleName.RolesName = "";
                }
                else
                {
                    ModelState.AddModelError("RoleName", "Role existing.");
                    ViewData[ROLECREATIONMESSAGE] = "Role existing.";
                }
            }
            return PartialView(RoleName);
        }

        [HttpPost]
        [Authorize(Roles = "System Administrator")]
        public String AjaxRemoveRole(RolesNameModel RoleName)
        {
            ViewData.Add(ROLECREATIONMESSAGE, "");
            if (ModelState.IsValid)
            {
                if (Roles.RoleExists(RoleName.RolesName))
                {
                    var users = Roles.GetUsersInRole(RoleName.RolesName);
                    if (users != null && users.Count() > 0)
                        Roles.RemoveUsersFromRole(
                            Roles.GetUsersInRole(RoleName.RolesName),
                            RoleName.RolesName);
                    Roles.DeleteRole(RoleName.RolesName);
                    ViewData[ROLECREATIONMESSAGE] = "Role Removed.";
                    RoleName.RolesName = "";
                }
                else
                {
                    ModelState.AddModelError("RoleName", "Role existing.");
                    ViewData[ROLECREATIONMESSAGE] = "Role existing.";
                }
            }
            return ViewData[ROLECREATIONMESSAGE].ToString();
        }

        [Authorize(Roles = "System Administrator")]
        public PartialViewResult RoleTablePartial()
        {
            return PartialView();
        }

        [Authorize(Roles = "System Administrator")]
        public PartialViewResult UserRequestTablePartial()
        {
            return PartialView();
        }
        [Authorize(Roles = "System Administrator")]
        public PartialViewResult UsersTablePartial(UserProfile UserProfile)
        {
            return PartialView(UserProfile);
        }


      
    }
}
