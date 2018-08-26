using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BusinessExcel.Controllers
{
    public partial class TargetController : Controller
    {

        public static string SALESEXECUTIVETARGET = "SalesExecutiveTarget";
        public static string SALES_EXECUTIVE_TARGET = "Sales Executive Target";

        [HttpGet]
        public ActionResult SalesExecutiveTarget()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + TARGETTEMPLATE;
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
            if (Request.IsAjaxRequest())
                return PartialView(SALESEXECUTIVETARGET,new SalesExcecutiveTarget());
            return View(new SalesExcecutiveTarget());
        }

        [HttpPost]
        public ActionResult SalesExecutiveTarget(SalesExcecutiveTarget SalesExcecutiveTarget, ICollection<LineTarget> lineTarget)
        {
            SalesExcecutiveTarget.LineTargets = lineTarget.ToArray();
            string message = ""; SalesExcecutiveTarget.Location = "141";
            SalesExcecutiveTarget.Save(out message);

            if (Request.IsAjaxRequest())
                return PartialView(SALESEXECUTIVETARGET,new SalesExcecutiveTarget());
            return View(new SalesExcecutiveTarget());
        }
    }
}
