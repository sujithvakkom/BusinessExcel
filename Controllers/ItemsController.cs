using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace BusinessExcel.Controllers
{
    public class ItemsController : Controller
    {
        //
        public static string ITEM_INDEX = "ItemIndex";
        public static string ITEM_FILTER = "ItemFilter";
        public static string ITEM_FILTER_VIEW = "_ItemFilter";
        public static string ITEM_TITLE = "Item Master";
        public static string ITEM_CONTROLLER = "Items";
        public static string SELECTED_FILTED_ITEM = "SelectedFilteredItem";
        public static string ITEM_MODEL_UPDATE = "getUpdatedItemModel";
        public static string UPDATE_CATOGERY = "UpdateCatogery";
        public static string ITEM_LINE = "ItemLine";
        public static string CREATECATEGORY = "CreateCategory";


        public ActionResult ItemIndex(ItemDetailsView Filters = null)
        {
            if (Filters == null)
            {
                Filters = new ItemDetailsView();
            }
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ITEM_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ITEM_TITLE;

            if (Request.IsAjaxRequest())
            {
                return PartialView(ITEM_INDEX, Filters);
            }
            return View(Filters);
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
        public JsonResult getUpdatedItemModel(ItemDetailsView itemModel)
        {
            int updated = 0;
            using (var db = new SalesManageDataContext())
            {
                updated = db.UpdateItemModel(itemModel);
            }
            return Json(updated, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult UpdateCatogery(ItemDetailsView item, string fieldPrefix)
        {
            if (!string.IsNullOrEmpty(fieldPrefix))
                UpdateModel(item, fieldPrefix);

            using (var db = new SalesManageDataContext())
            {
                var updated = db.UpdateItemCat(item);
                int count;
                item = db.ItemDetailsPaging(1, 1, "", "", out count, item).First();
            }
            return PartialView(ITEM_LINE, item);
        }

        [HttpPost]
        public String CreateCategory(string CategoryDescrption)
        {
            if (string.IsNullOrWhiteSpace(CategoryDescrption)) return "Invalida data";

            using (var db = new SalesManageDataContext())
            {
                var updated = db.AddCategoryDetails(CategoryDescrption);
                if (updated == null) return "Please check exists or not.";
            }
            return "Success";
        }
    }
}