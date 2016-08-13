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
    public class PublicController : Controller
    {
        public const string PUBLIC = "Public";
        //
        // GET: /Public/Welcome
        public const string WELCOME = "Welcome";
        [HttpGet]
        public ActionResult Welcome()
        {
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
        public ActionResult Welcome(LoginModel Model)
        {
            return RedirectToAction("Home", "Accounts");
        }

        //GET: /Public/Register
        public const string REGISTER = "Register";
        [HttpGet]
        public ActionResult Register()
        {
            WebSecurity.CreateAccount()
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel Model)
        {
            return Welcome();
        }

        //GET: /Public/ForgotPassword
        public const string FORGOT_PASSWORD= "ForgotPassword";
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }


    }
}
