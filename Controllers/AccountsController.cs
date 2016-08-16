using BusinessExcel.Filters;
using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel.Controllers
{
    //[Authorize]
    //[InitializeSimpleMembership]
    public class AccountsController : Controller
    {
        public const string ACCOUNTS = "Accounts";

        public const string WELCOME = "Home";
        // GET: /Accounts/Home
        public ActionResult Home()
        {
            using(var db = new UsersContext()){
                ViewBag.UserProfile = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name);
            }
            return View();
        }

        public const string PROFILE = "Profile";
        //GET: /Account/Profile
        public ActionResult Profile()
        {
            return View();
        }
    }
}
