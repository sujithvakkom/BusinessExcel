using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public partial class ReportController : Controller
    {
        //
        // GET: /Report/
        public static string RosterActions_TITLE = "Roster List";
        public static string ROSTER_REPORTCONTROLLER = "Report";
        public static string ROSTER_ACTIONS = "RosterActions";

        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public ActionResult RosterActions(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + RosterActions_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;

            if (!string.IsNullOrEmpty(Filters.UserName))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(Filters.UserName);
                }

            if (!string.IsNullOrEmpty(Filters.Location))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.Location);
                }
            if (Request.IsAjaxRequest())
            {
                //return PartialView(TABLEDAILYUPATEVIEW, Filters);
                return (PartialViewResult)TableDailyUpateView(sort, sortdir, page, Filters);
            }
            return View();
        }

        //
        // GET: /Report/
        public static string TABLEROSTERUPATEVIEW = "TableRosterUpateView";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        [HttpGet]
        public PartialViewResult TableRosterUpateView(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            return PartialView(TABLEROSTERUPATEVIEW, Filters);
        }

      

    }
}