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
            using (var db = new UsersContext())
            {
                try
                {
                    Session[Index.USER_PROFILE_INDEX] = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;
                }
                catch (Exception)
                {
                    WebSecurity.Logout();
                    RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
                }
            }
            
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            return View();
        }

        public const string PROFILE = "Profile";
        public const string MYPROFILE = "MyProfile";
        //GET: /Account/MyProfile
        [HttpGet]
        public ActionResult MyProfile()
        {
            UserProfile profile = null;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            using (var db = new UsersContext())
            {
                try
                {
                    profile = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name);
                }
                catch (Exception)
                {
                    WebSecurity.Logout();
                    RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
                }
            }
            return View(profile);
        }

        [HttpPost]
        public ActionResult MyProfile(UserProfile UserProfile)
        {using (var db = new UsersContext())
            {
                try
                {
                    var profile = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name);
                    profile.UserFullName = UserProfile.UserFullName;
                    db.SaveChanges();
                    Session[Index.USER_PROFILE_INDEX] = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;
                }
                catch (Exception)
                {
                    WebSecurity.Logout();
                    RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
                }
            }
            return RedirectToAction(WELCOME, ACCOUNTS);
        }
    }
}
