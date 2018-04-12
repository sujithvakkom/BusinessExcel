using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult UserManagement()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERMANAGEMENT_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = USERMANAGEMENT_TITLE;
            RoleContext db =new RoleContext();

            ViewBag.Roles = db.Roles;
            return View();
        }

    }
}
