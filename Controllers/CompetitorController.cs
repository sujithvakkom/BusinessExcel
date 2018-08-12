using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public class CompetitorController : Controller
    {
        //
        // GET: /Competitor/
        public static string COMPETITOR_INDEX = "CompetitorIndex";
        public static string COMPETITOR_FILTER = "CompetitorFilter";
        public static string COMPETITOR_FILTER_VIEW = "_CompetitorFilter";
        public static string COMPETITOR_TITLE = "Competitor Activity";
        public static string COMPETITOR_CONTROLLER = "Competitor";
        public static string SELECTED_FILTED_USER = "SelectedFilteredUser";
        public static string COMPETITOR_MODEL_UPDATE = "getUpdatedCompetitorModel";
        public ActionResult CompetitorIndex()
        {
          
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + COMPETITOR_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = COMPETITOR_TITLE;

            return View(new CompetitorViewModel());
        }
        [HttpGet]
        public PartialViewResult CompetitorFilter(string sort, string sortdir, int page = 1, CompetitorViewModel Filters = null)
        {
            ViewBag.CompetitorViewSort = sort;
            ViewBag.CompetitorViewDir = sortdir;
            ViewBag.CompetitorViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + COMPETITOR_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = COMPETITOR_TITLE;

            if (!string.IsNullOrEmpty(Filters.user_name))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(Filters.user_name);
                }

            return PartialView(COMPETITOR_FILTER_VIEW, Filters);

        }

    }
}
