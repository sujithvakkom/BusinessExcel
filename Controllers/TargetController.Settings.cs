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
        public static string SETTING_TARGET = "Target Settings";

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
        public static string _SETTING_LOCATION = "Location Settings";

        [HttpGet]
        //public ActionResult _SettingLocation()
        //{
        //    ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + TARGETTEMPLATE;
        //    using (var db = new UsersContext())
        //    {
        //        try
        //        {
        //            Session[Index.USER_PROFILE_INDEX] = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;
        //        }
        //        catch (Exception)
        //        {
        //            WebSecurity.Logout();
        //            RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
        //        }
        //    }
        //    ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
        //    if (Request.IsAjaxRequest())
        //        return PartialView(SETTINGTARGET);
        //    return View();
        //}
        //[HttpPost]
        public PartialViewResult _SettingLocation(string sort="", string sortdir="", int page = 1)
        {
            ViewBag.Page = page;
            return PartialView();
        }

        public static string _SETTINGINCENTIVEFACTORDETAILS = "_SettingIncentiveFactorDetails";
        public static string _SETTING_INCENTIVE_FACTOR_DETAILS = "Incentive Factor Details";

        [HttpGet]
        //public ActionResult _SettingIncentiveFactorDetails()
        //{
        //    ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + TARGETTEMPLATE;
        //    using (var db = new UsersContext())
        //    {
        //        try
        //        {
        //            Session[Index.USER_PROFILE_INDEX] = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;
        //        }
        //        catch (Exception)
        //        {
        //            WebSecurity.Logout();
        //            RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
        //        }
        //    }
        //    ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
        //    if (Request.IsAjaxRequest())
        //        return PartialView(_SETTINGINCENTIVEFACTORDETAILS);
        //    return View();
        //}

        //[HttpPost]
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
            if (ModelState.IsValid)
                using (var db = new SalesManageDataContext())
                {
                    try
                    {
                        db.insertUpdateIncentiveFactor(IncentiveFactor.Account, IncentiveFactor.Line, IncentiveFactor.Factor, IncentiveFactor.Threshold);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Errors = new string[] { ex.Message };
                        ViewBag.IncentiveFactor = IncentiveFactor;
                    }
                }
            else
                ViewBag.IncentiveFactor = IncentiveFactor;
            return PartialView(_SETTINGINCENTIVEFACTORDETAILS);
        }

        public static string _CATEGORYSETTINGS = "_CategorySettings";
        public static string _CATEGORY_SETTINGS = "Category Settings";

        public PartialViewResult _CategorySettings(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.ViewSort = sort;
            ViewBag.ViewDir = sortdir;
            ViewBag.Page = page;
            return PartialView(_CATEGORYSETTINGS);
        }

        public static string _ADDCATEGORYSETTINGS = "_AddCategorySettings";
        public static string _ADD_CATEGORY_SETTINGS = "Add Category Settings";

        [HttpGet]
        public PartialViewResult _AddCategorySettings()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult _AddCategorySettings(CategoryDetail CategoryDetail)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SalesManageDataContext())
                {
                    try
                    {
                        db.insertUpdateCategorysettings(CategoryDetail.category_id, CategoryDetail.description, CategoryDetail.base_incentive, CategoryDetail.total_sale_factor,CategoryDetail.category_sellin_factor);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Errors = new string[] { ex.Message };
                        ViewBag.CategoryDetail = CategoryDetail;
                    }
                }
            }
            else
            {
                ViewBag.CategoryDetail = CategoryDetail;
            }
            return PartialView(_CATEGORYSETTINGS);
        }

        public static string _OTHERSETTINGS = "_OtherSettings";
        public static string _OTHER_SETTINGS = "Global Settings";

        [HttpGet]
        public PartialViewResult _OtherSettings()
        {
            using (var db = new SalesManageDataContext())
            {
                return PartialView(db.getGlobalSettings());
            }
        }

        [HttpPost]
        public PartialViewResult _OtherSettings(GlobalSettings GlobalSettings)
        {

            GlobalSettings.base_incentive_pct /= 100;
            GlobalSettings.bonus_achieve_acc /= 100;
            GlobalSettings.global_acc_factor /= 100;
            GlobalSettings.line_achieve_acc /= 100;
            GlobalSettings.min_line_achievement /= 100;
            GlobalSettings.min_total_achievement /= 100;

            if (ModelState.IsValid)
            {
                using (var db = new SalesManageDataContext())
                {
                    try
                    {
                        db.insertUpdateGlobalSettings(GlobalSettings);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Errors = new string[] { ex.Message };
                    }
                }

            }
            return PartialView(GlobalSettings);
        }

    }
}
