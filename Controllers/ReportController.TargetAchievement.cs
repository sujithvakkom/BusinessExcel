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
        public static string TARGETACHIEVEMENT_ACTIONS_TITLE = "TargetAchievement";
       // public static string REPORTCONTROLLER = "Report";
        public static string TARGETACHIEVEMENTACTIONS = "TargetAchievementActions";
      ////  public static string SELECTED_FILTED_ITEM1 = "SelectedFilteredItem";
      //  public static string SELECTED_FILTED_USER1 = "SelectedFilteredUser";
      //  public static string SELECTED_FILTED_BRAND11 = "SelectedFilteredBrand";
      //  public static string SELECTED_FILTED_LOCATION1 = "SelectedFilteredLocation";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public ActionResult TargetAchievementActions(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.TargetAchievementeViewSort = sort;
            ViewBag.TargetAchievementViewDir = sortdir;
            ViewBag.TargetAchievementViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            if (!string.IsNullOrEmpty(Filters.ItemCode))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_ITEM] = db.getItemDetails(Filters.ItemCode);
                }
            if (!string.IsNullOrEmpty(Filters.UserName))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(Filters.UserName);
                }
            if (!string.IsNullOrEmpty(Filters.BrandID))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_BRAND] = db.getBrandDetail(Filters.BrandID);
                }
            if (!string.IsNullOrEmpty(Filters.Location))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.Location);
                }
            if (Request.IsAjaxRequest())
            {
                //return PartialView(TABLEDAILYUPATEVIEW, Filters);
                return (PartialViewResult)TargetAchievementView(sort, sortdir, page, Filters);
            }
            return View();
        }

        //
        // GET: /Report/
       public static string TARGETACHIEVEMENTVIEW = "TargetAchievementView";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        [HttpGet]
        public PartialViewResult TargetAchievementView(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            return PartialView(TARGETACHIEVEMENTVIEW, Filters);
        }

        public static string EXPORTEXCEL_TARGETACHIEVEMENT= "ExportExcel_TargetAchievement";
       // public static string EXPORTEXCEL_TITLE = "Export Excel";
        public ActionResult ExportExcel_TargetAchievement(ActionViewFilters Filters = null)
        {

            using (var db = new SalesManageDataContext())
            {
                var gv = new System.Web.UI.WebControls.GridView();
                gv.DataSource = db.GetTargetAchievementViewPagingExport(Filters);
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename= TargetAchievement.xls");
                Response.ContentType = "application/ms-excel";
                Response.Write("<style> TD { mso-number-format:\\@; } </style>");
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                System.Web.UI.HtmlTextWriter objHtmlTextWriter = new System.Web.UI.HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View("Index");
        }
    }
}