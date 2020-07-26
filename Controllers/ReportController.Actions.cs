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
        public static string ACTIONS_TITLE = "Actions";
        public static string REPORTCONTROLLER = "Report";
        public static string ACTIONS = "Actions";
        public static string SELECTED_FILTED_ITEM = "SelectedFilteredItem";
        public static string SELECTED_FILTED_USER = "SelectedFilteredUser";
        public static string SELECTED_FILTED_BRAND = "SelectedFilteredBrand";
        public static string SELECTED_FILTED_LOCATION = "SelectedFilteredLocation";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public ActionResult Actions(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            if (Filters == null) Filters = new ActionViewFilters();
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
                return (PartialViewResult)TableDailyUpateView(sort, sortdir, page, Filters);
            }
            return View(Filters);
        }

        //
        // GET: /Report/
        public static string TABLEDAILYUPATEVIEW = "TableDailyUpateView";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        [HttpGet]
        public PartialViewResult TableDailyUpateView(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            if (Filters == null) Filters = new ActionViewFilters();
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ACTIONS_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ACTIONS_TITLE;
            return PartialView(TABLEDAILYUPATEVIEW, Filters);
        }

        public static string EXPORTEXCEL = "ExportExcel";
        public static string EXPORTEXCEL_TITLE = "Export Excel";
        public ActionResult ExportExcel(ActionViewFilters Filters = null)
        {
            if (Filters == null) Filters = new ActionViewFilters();
            using (var db = new SalesManageDataContext())
            {
                var gv = new System.Web.UI.WebControls.GridView();
                int count = 0;
                var result = db.DailyUpateViewPaging(1, int.MaxValue, "", "", out count, Filters).ToList();
                gv.DataSource =
                    result.Where(m => m.CreateTime != null).Select(m => new
                    {
                        Name = m.Name,
                        Code = m.UserName,
                        Location = m.Location,
                        Brand = m.Brand,
                        Category = m.Category,
                        Date = m.CreateTime.Value.Date,
                        Item = m.Item,
                        Description = m.Description,
                        Quantity = m.Quantity,
                        Value = m.TotalValue
                    });
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