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
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        public static string ACTIONS_TITLE = "Actions";
        public static string REPORTCONTROLLER = "Report";
        public static string ACTIONS = "Actions";
        public static string SELECTED_FILTED_ITEM = "SelectedFilteredItem";
        public static string SELECTED_FILTED_USER = "SelectedFilteredUser";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public ActionResult Actions(string sort,string sortdir, int page = 1,ActionViewFilters Filters=null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            if (Request.IsAjaxRequest())
            {
                return TableDailyUpateView(sort, sortdir, page,Filters);
            }
            return View();
        }

        //
        // GET: /Report/
        public static string TABLEDAILYUPATEVIEW = "TableDailyUpateView";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        [HttpGet]
        public PartialViewResult TableDailyUpateView(string sort, string sortdir, int page = 1, ActionViewFilters Filters=null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
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
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetails(Filters.UserName);
                }
            return PartialView(TABLEDAILYUPATEVIEW,Filters);
        }

        public static string EXPORTEXCEL="ExportExcel";
        public static string EXPORTEXCEL_TITLE = "Export Excel";
        public ActionResult ExportExcel(ActionViewFilters Filters = null)
        {

            using (var db = new SalesManageDataContext())
            {
                var gv = new System.Web.UI.WebControls.GridView();
                gv.DataSource = db.GetDailyUpateViewPagingExport(Filters);
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
                Response.ContentType = "application/ms-excel";
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