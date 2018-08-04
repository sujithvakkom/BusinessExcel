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
    public class ItemsController : Controller
    {
        //
        public static string ITEM_INDEX= "ItemIndex";
        public static string ITEM_FILTER = "ItemFilter";
        public static string ITEM_FILTER_VIEW = "_ItemFilter";
        public static string ITEM_TITLE = "Item Master";
        public static string ITEM_CONTROLLER = "Items";
        public static string SELECTED_FILTED_ITEM = "SelectedFilteredItem";

        public ActionResult ItemIndex()
        {
            //ViewBag.ItemViewSort = sort;
            //ViewBag.ItemViewDir = sortdir;
            //ViewBag.ItemViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ITEM_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ITEM_TITLE;

            return View(new ItemDetailsView( ));
        }
        [HttpGet]
        public PartialViewResult ItemFilter(string sort, string sortdir, int page = 1, ItemDetailsView Filters = null)
        {
            ViewBag.ItemViewSort = sort;
            ViewBag.ItemViewDir = sortdir;
            ViewBag.ItemViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ITEM_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ITEM_TITLE;

            if (!string.IsNullOrEmpty(Filters.item_code))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_ITEM] = db.getItemDetails(Filters.item_code);
                }

            return PartialView(ITEM_FILTER_VIEW, Filters);

        }

    }
}
