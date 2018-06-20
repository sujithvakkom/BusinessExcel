
using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using System.Configuration;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public partial class ReportController : Controller
    {

        //
        // GET: /Report/
        public static string CATEGORYACHIEVEMENT_TITLE = "Category vise Achievement";
        public static string CATEGORYACHIEVEMENT = "CategoryAchievement";
        public ActionResult CategoryAchievement(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            if (!string.IsNullOrEmpty(Filters.UserName))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(Filters.UserName);
                }
            if (Request.IsAjaxRequest())
            {
                //return PartialView(TABLEDAILYUPATEVIEW, Filters);
                return (PartialViewResult)TableCategoryAchievementView(sort, sortdir, page, Filters);
            }
            return View();
        }

        public static string TABLECATEGORYACHIEVEMENTVIEW = "TableCategoryAchievementView";
        [HttpGet]
        public PartialViewResult TableCategoryAchievementView(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            return PartialView(TABLECATEGORYACHIEVEMENTVIEW, Filters);
        }
    }
}