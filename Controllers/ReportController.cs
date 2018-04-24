using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        public static string ACTIONS_TITLE = "Actions";
        public static string REPORTCONTROLLER = "Report";
        public static string ACTIONS = "Actions";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public ActionResult Actions(string sort,string sortdir, int page = 1)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            return View();
        }

    }
}