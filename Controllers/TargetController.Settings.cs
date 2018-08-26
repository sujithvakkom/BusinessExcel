using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BusinessExcel.Controllers
{
    public partial class TargetController : Controller
    {
        public static string SETTINGTARGET = "SettingTarget";
        public static string SETTING_TARGET = "Setting Target";

        [HttpGet]
        public ActionResult SettingTarget()
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
                return PartialView(SETTINGTARGET);
            return View();
        }

        public static string _SETTINGLOCATION = "_SettingLocation";
        public static string _SETTING_LOCATION = "Location Setting";

        [HttpGet]
        public ActionResult _SettingLocation()
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
                return PartialView(SETTINGTARGET);
            return View();
        }
        [HttpPost]
        public PartialViewResult _SettingLocation(string sort, string sortdir, int page = 1)
        {
            ViewBag.Page = page;
            return PartialView();
        }

        public static string _SETTINGINCENTIVEFACTORDETAILS = "_SettingIncentiveFactorDetails";
        public static string _SETTING_INCENTIVE_FACTOR_DETAILS = "Incentive Factor Details";

        [HttpGet]
        public ActionResult _SettingIncentiveFactorDetails()
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
                return PartialView(_SETTINGINCENTIVEFACTORDETAILS);
            return View();
        }

        [HttpPost]
        public PartialViewResult _SettingIncentiveFactorDetails(string sort, string sortdir, int page = 1)
        {
            ViewBag.Page = page;
            return PartialView();
        }

        public static string _ADDINCENTIVEFACTORDETAILS = "_AddIncentiveFactorDetails";
        public static string _ADD_INCENTIVE_FACTOR_DETAILS = "_AddIncentiveFactorDetails";

        [HttpGet]
        public PartialViewResult _AddIncentiveFactorDetails()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult _AddIncentiveFactorDetails(BusinessExcel.Models.IncentiveFactor IncentiveFactor)
        {
            using (var db = new SalesManageDataContext()) {
                try
                {
                    db.insertUpdateIncentiveFactor(IncentiveFactor.Account, IncentiveFactor.Line, IncentiveFactor.Factor,IncentiveFactor.Threshold);
                }
                catch(Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return PartialView("_SettingIncentiveFactorDetails");
        }
    }
}
