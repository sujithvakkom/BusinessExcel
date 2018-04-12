using BusinessExcel.Models;
using BusinessExcel.Providers;
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
    [Authorize(Roles = BERoleDetails.SYSTEM_ADMINISTRATOR)]
    public class AdminController : Controller
    {
        public const string ADMIN = "Admin";

        public static string USERMANAGEMENT = "UserManagement";
        public static string USERMANAGEMENT_TITLE = "User Management";
        //
        // GET: /Admin/UserManagement

        [Authorize(Roles = "System Administrator")]
        [HttpGet]
        public ActionResult UserManagement()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERMANAGEMENT_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = USERMANAGEMENT_TITLE;

            return View();
        }

        [HttpPost]
        public PartialViewResult AjaxCreateRole(RolesNameModel RoleName)
        {
            if (ModelState.IsValid)
            {
                if (!Roles.RoleExists(RoleName.RolesName))
                    Roles.CreateRole(RoleName.RolesName);
                else
                    ModelState.AddModelError("RoleName", "Role existing.");
            }
            return PartialView(RoleName);
        }
    }
}
