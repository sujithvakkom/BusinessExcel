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
    [AllowAnonymous]
    [InitializeSimpleMembership]
    public class PublicController : Controller
    {
        public const string PUBLIC = "Public";

        public const string WELCOME = "Welcome";
        // GET: /Public/Welcome
        [HttpGet]
        public ActionResult Welcome()
        {
            ViewBag.Title = WELCOME;
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Home", "Accounts");
            }
            else
            {
                return View(new LoginModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Welcome(LoginModel Model)
        {
            ViewBag.Title = WELCOME;
            if (ModelState.IsValid && WebSecurity.Login(Model.Email, Model.Password, Model.RememberMe))
                return RedirectToAction("Home", "Accounts");
            else
                ModelState.AddModelError("Invalid Login", "User Name or Password are wrong, Please try again Or Register.");
            return View(Model);
        }

        public const string REGISTER = "Register";
        //GET: /Public/Register
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Title = REGISTER;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel Model)
        {
            ViewBag.Title = REGISTER;
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(Model.Email, Model.Password);
                    using (var db = new UsersContext())
                    {
                        UserProfile user = db.UserProfiles.SingleOrDefault(x => x.UserName == Model.Email);
                        user.UserFullName = Model.RegFullName;
                        db.SaveChanges();
                    }
                    WebSecurity.Login(Model.Email, Model.Password);
                    return RedirectToAction("Home", "Accounts");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(Model);
        }

        //GET: /Public/ForgotPassword
        public const string FORGOT_PASSWORD= "Forgot Password";
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ViewBag.Title = FORGOT_PASSWORD;
            return View();
        }

        public const string LOGOFF = "Logoff";
        // GET: /Accounts/Logoff
        [HttpGet]
        public ActionResult Logoff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Welcome");
        }
        
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
