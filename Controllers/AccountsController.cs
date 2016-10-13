using BusinessExcel.Filters;
using BusinessExcel.Models;
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
    //[Authorize]
    //[InitializeSimpleMembership]
    public class AccountsController : Controller
    {
        public const string ACCOUNTS = "Accounts";

        public const string WELCOME = "Home";

        public const string WELCOME_TITLE = "Home";
        // GET: /Accounts/Home
        public ActionResult Home()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] +" | "+ WELCOME_TITLE;
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
        public const string MYPROFILE_TITLE = "Profile";
        //GET: /Account/MyProfile
        [HttpGet]
        public ActionResult MyProfile()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] +" | "+ MYPROFILE_TITLE;
            EditUserProfile profile = null;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            using (var db = new UsersContext())
            {
                try
                {
                    var userProfile = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name);
                    profile = new EditUserProfile()
                    {
                        UserName = userProfile.UserName,
                        UserFullName = userProfile.UserFullName
                    };
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
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(EditUserProfile EditUserProfile)
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + MYPROFILE_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            if (ModelState.IsValid)
            {
                using (var db = new UsersContext())
                {
                    try
                    {
                        var profile = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name);
                        profile.UserFullName = EditUserProfile.UserFullName;
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
            return View(EditUserProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult ChangeName(EditUserProfile EditUserProfile)
        {
            string message = "";
            bool successFlag = false;
            List<string> errors = new List<string>();
            if (EditUserProfile.CurrentPassword != null && EditUserProfile.CurrentPassword.Length > 0)
                if (Membership.ValidateUser(HttpContext.User.Identity.Name, EditUserProfile.CurrentPassword))
                    using (var db = new UsersContext())
                    {
                        try
                        {
                            var profile = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name);
                            profile.UserFullName = EditUserProfile.UserFullName;
                            db.SaveChanges();
                            Session[Index.USER_PROFILE_INDEX] = db.UserProfiles.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;

                            message = "Profile Name Change Success.";
                            successFlag = true;
                        }
                        catch (Exception ex)
                        {
                            message = "Profile Name Change Failed.";
                            errors.Add(ex.Message);
                        }
                    }
                else
                {
                    errors.Add("Wrong Password.");
                    message = "Wrong Password.";
                }
            else
            {
                errors.Add("Password field cannot be blank.");
            }
            EditUserProfile.CurrentPassword = "";
            ViewBag.Message = message;
            ViewBag.Errors = errors;
            ViewBag.SuccessFlag = successFlag;
            return PartialView("_ResultMessage");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult ChangePassword(EditUserProfile EditUserProfile)
        {
            string message = "";
            bool successFlag = false;
            List<string> errors = new List<string>();
            if (EditUserProfile.CurrentPassword != null &&
                EditUserProfile.NewPassword != null &&
                EditUserProfile.ConfirmPassword != null &&
                EditUserProfile.NewPassword == EditUserProfile.ConfirmPassword &&
                EditUserProfile.CurrentPassword.Length > 0 &&
                EditUserProfile.NewPassword.Length > 0 &&
                EditUserProfile.ConfirmPassword.Length > 0)
            {
                if (Membership.ValidateUser(HttpContext.User.Identity.Name, EditUserProfile.CurrentPassword))
                {
                    if(
                    WebSecurity.ChangePassword(User.Identity.Name, EditUserProfile.CurrentPassword, EditUserProfile.NewPassword))
                    {
                        successFlag=true;
                        message = "Password changed successfully.";
                    }
                    else 
                        message = "Password change failed.";
                }
                else
                {
                    errors.Add("Wrong Password.");
                    message = "Wrong Password.";
                }
            }
            else
            {
                if (EditUserProfile.CurrentPassword != null ||
                    EditUserProfile.CurrentPassword.Length > 0)
                    errors.Add("Current password field cannot be blank.");
                if (EditUserProfile.NewPassword != null ||
                    EditUserProfile.NewPassword.Length > 0)
                    errors.Add("New password field cannot be blank.");
                if (EditUserProfile.ConfirmPassword != null ||
                    EditUserProfile.ConfirmPassword.Length > 0)
                    errors.Add("Confirm password field cannot be blank.");
                if( EditUserProfile.NewPassword == EditUserProfile.ConfirmPassword)
                    errors.Add("New and confirm passwords should be same");
            }
            EditUserProfile.CurrentPassword = "";
            ViewBag.Message = message;
            ViewBag.Errors = errors;
            ViewBag.SuccessFlag = successFlag;
            return PartialView("_ResultMessage");
        }

        [HttpPost]
        public string GetUsername()
        {
            return (String)Session[Index.USER_PROFILE_INDEX];
        }
    }
}
