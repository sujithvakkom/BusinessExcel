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
        //
        // GET: /Public/
        [HttpGet]
        public ActionResult Welcome()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Home", "Accounts");
            }
            else
            {
                return View(new RegisterModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel Login)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(Login.Email, true);
                return RedirectToAction("Home", "Accounts");
                //return View("Welcome", new RegisterModel() { RegEmail = Login.Email, RegPassword = Login.Password });
            }
            else
            {
                ModelState.AddModelError("Email", new Exception("Email is Required Field"));
                return View("Welcome", new RegisterModel() { RegEmail = Login.Email, RegPassword = Login.Password });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Register(RegisterModel Login)
        {
            var result = new List<object>();
            if (ModelState.IsValid)
                WebSecurity.CreateUserAndAccount(Login.RegEmail, Login.RegPassword, false, false);
            result.Add(
                new
                {
                    IsAuthenticated = true,
                    ReturnUrl = "~/Accounts/Home"
                }
                );
            return Json(result);
        }

        public PartialViewResult _LodinPartial(LoginModel Login)
        {
            return PartialView(Login);
        }
    }
}
