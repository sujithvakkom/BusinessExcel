using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    [Authorize(Roles=BERoleDetails.RoleName[BERoles.SystemAdministrator])]
    public class AdminController : Controller
    {
        public static string USERMANAGEMENT = "UserManagement"
        public static string USERMANAGEMENT_TITLE = "User Management"
        //
        // GET: /Admin/UserManagement

        public ActionResult UserManagement()
        {
            ViewBag.Title = USERMANAGEMENT_TITLE;
            return View();
        }

    }
}
