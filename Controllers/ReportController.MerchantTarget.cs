using BusinessExcel.Models;
using BusinessExcel.Providers;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

using System.Data.Entity;
using System.IO;
using System.Net;

namespace BusinessExcel.Controllers
{
    public partial class ReportController : Controller
    {

        public static string MERCHAND_TARGET = "Merchant Target";
        public static string MERCHANDTARGET = "MerchantTarget";
        [HttpGet]
        public ActionResult MerchantTarget()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            if (Request.IsAjaxRequest())
            {
                //return PartialView(TABLEDAILYUPATEVIEW, Filters);
                return PartialView(MERCHANDTARGET, new MerchantTargetFilter());
            }
            return View(new MerchantTargetFilter());
        }

        [HttpPost]
        public ActionResult MerchantTarget(MerchantTargetFilter filter)
        {
            if (Request.IsAjaxRequest())
            {
                //return PartialView(TABLEDAILYUPATEVIEW, Filters);
                return PartialView(MERCHANDTARGET, filter);
            }
            return View(filter);
        }

        public static string _MERCHANTTARGETTABLE = "_MerchantTargetTable";
        public PartialViewResult _MerchantTargetTable(MerchantTargetFilter filter)
        {
            return PartialView(_MERCHANTTARGETTABLE, filter);
        }
    }
}
