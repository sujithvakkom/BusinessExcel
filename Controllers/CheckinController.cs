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
    public class CheckinController : Controller
    {
        //
        // GET: /Checkin/
        public static string CHECKIN_INDEX = "CheckinIndex";
        public static string CHECKIN_DETAILS = "CheckinDetails";
        public static string CHECKIN_DETAILS_VIEW = "_CheckinDetails";
        public static string CHECKIN_TITLE = "Checkin Details";
        public static string CHECKIN_CONTROLLER = "Checkin";
        public static string SELECTED_FILTED_USER = "SelectedFilteredUser";


        public ActionResult CheckinIndex()
        {

            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + CHECKIN_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = CHECKIN_TITLE;

            return View(new CheckinViewModel());
            
        }

        [HttpGet]
        public PartialViewResult CheckinDetails(string sort, string sortdir, int page = 1, CheckinViewModel Filters = null)
        {
            ViewBag.ItemViewSort = sort;
            ViewBag.ItemViewDir = sortdir;
            ViewBag.ItemViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + CHECKIN_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = CHECKIN_TITLE;

            if (!string.IsNullOrEmpty(Filters.user_name))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(Filters.user_name);
                }

            return PartialView(CHECKIN_DETAILS_VIEW, Filters);

        }
        
    }
}
