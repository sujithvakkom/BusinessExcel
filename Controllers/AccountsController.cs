using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel.Controllers
{
    //[Authorize]
    //[InitializeSimpleMembership]
    public partial class AccountsController : Controller
    {
        public const string ACCOUNTS = "Accounts";

        public const string WELCOME = "Home";

        public const string WELCOME_TITLE = "Home";
        // GET: /Accounts/Home
        public ActionResult Home()
        {
            if (!Roles.RoleExists("System Administrator")) Roles.CreateRole("System Administrator");
            if (!Roles.GetRolesForUser().Contains("System Administrator")&& WebSecurity.CurrentUserName=="sujithvakkom@gmail.com")
                Roles.AddUserToRole(WebSecurity.CurrentUserName, "System Administrator");
            
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] +" | "+ WELCOME_TITLE;
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
            ViewBag.QtyGraph = GetGraphForCurrentMonthQuantity();
            ViewBag.ValueGraph = GetGraphForCurrentMonthValue();
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            if (Request.IsAjaxRequest())
                return PartialView(WELCOME);

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
                    var userProfile = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name);
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
            if (Request.IsAjaxRequest())
                return PartialView(MYPROFILE,profile);
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
                        var profile = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name);
                        profile.UserFullName = EditUserProfile.UserFullName;
                        db.SaveChanges();
                        Session[Index.USER_PROFILE_INDEX] = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;
                    }
                    catch (Exception)
                    {
                        WebSecurity.Logout();
                        RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
                    }
                }
                return RedirectToAction(WELCOME, ACCOUNTS);
            }
            return PartialView(EditUserProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult ChangeName(EditUserProfile ChangeName)
        {
            string message = "";
            bool successFlag = false;
            List<string> errors = new List<string>();
            if (ChangeName.CurrentPassword != null && ChangeName.CurrentPassword.Length > 0)
                if (Membership.ValidateUser(HttpContext.User.Identity.Name, ChangeName.CurrentPassword))
                    using (var db = new UsersContext())
                    {
                        try
                        {
                            var profile = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name);
                            profile.UserFullName = ChangeName.UserFullName;
                            db.SaveChanges();
                            Session[Index.USER_PROFILE_INDEX] = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;

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
            ChangeName.CurrentPassword = "";
            ViewBag.Message = message;
            ViewBag.Errors = errors;
            ViewBag.SuccessFlag = successFlag;
            return PartialView("_ChangeUsernameBlock", ChangeName);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult ChangePassword(EditUserProfile ChangePassword)
        {
            string message = "";
            bool successFlag = false;
            List<string> errors = new List<string>();
            if (ChangePassword.CurrentPassword != null &&
                ChangePassword.NewPassword != null &&
                ChangePassword.ConfirmPassword != null &&
                ChangePassword.NewPassword == ChangePassword.ConfirmPassword &&
                ChangePassword.CurrentPassword.Length > 0 &&
                ChangePassword.NewPassword.Length > 0 &&
                ChangePassword.ConfirmPassword.Length > 0)
            {
                if (Membership.ValidateUser(HttpContext.User.Identity.Name, ChangePassword.CurrentPassword))
                {
                    if(
                    WebSecurity.ChangePassword(User.Identity.Name, ChangePassword.CurrentPassword, ChangePassword.NewPassword))
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
                if (ChangePassword.CurrentPassword == null ||
                    ChangePassword.CurrentPassword.Length > 0)
                    errors.Add("Current password field cannot be blank.");
                if (ChangePassword.NewPassword == null ||
                    ChangePassword.NewPassword.Length > 0)
                    errors.Add("New password field cannot be blank.");
                if (ChangePassword.ConfirmPassword == null ||
                    ChangePassword.ConfirmPassword.Length > 0)
                    errors.Add("Confirm password field cannot be blank.");
                if( ChangePassword.NewPassword == ChangePassword.ConfirmPassword)
                    errors.Add("New and confirm passwords should be same");
            }
            ChangePassword.CurrentPassword = "";
            ViewBag.Message = message;
            ViewBag.Errors = errors;
            ViewBag.SuccessFlag = successFlag;
            return PartialView("_ChangeUserPasswordBlock",ChangePassword);
        }

        [HttpPost]
        public string GetUsername()
        {
            return (String)Session[Index.USER_PROFILE_INDEX];
        }

        [HttpGet]
        public ActionResult UpdateLocation()
        {
            return View(new UserLocation() { });
        }

        [HttpPost]
        public ActionResult UpdateLocation(UserLocation UserLocation) {
            return View(UserLocation);
        }
    }
}
