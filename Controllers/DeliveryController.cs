using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;

namespace BusinessExcel.Controllers
{
    public class DeliveryController : Controller
    {
        //
        // GET: /Delivery/


        public static string DELIVERYCONTROLLER = "Delivery";
       
        //public static string DELIVERY_INDEX = "DeliveryList";
       // public static string ROSTER_EDIT = "EditRoster";
        public static string DELIVERY_TITLE = "Delivery";
        public static string DELIVERY__ACTION = "DeliveryIndex";
        // public static string AJAXCREATEROSTER = "AjaxCreateRoster";
        //  public static string ROSTERCREATIONMESSAGE = "RosterCreationMessage";
        //  public static string AJAXDELETEROSTER = "AjaxDeleteRoster";

        public static string SELECTED_FILTED_ITEM = "SelectedFilteredItem";
        public static string SELECTED_FILTED_USER = "SelectedFilteredUser";
        public static string SELECTED_FILTED_Contacts = "SelectedFilteredContacts";
        public static string SELECTED_FILTED_LOCATION = "SelectedFilteredLocation";

        //[Authorize(Roles = "Manager")]
        [Authorize(Roles = "Delivery Manager")]
        [HttpGet]
        public ActionResult DeliveryIndex(string sort, string sortdir, int page = 1, DeliveryFilters Filters = null)
        {
            ViewBag.DeliveryUpateViewSort = sort;
            ViewBag.DeliveryUpateViewDir = sortdir;
            ViewBag.DeliveryUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + DELIVERY_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = DELIVERY_TITLE;
            if (!string.IsNullOrEmpty(Filters.ItemCode))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_ITEM] = db.getItemDetails(Filters.ItemCode);
                }
            //if (!string.IsNullOrEmpty(Filters.UserName))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        ViewData[SELECTED_FILTED_USER] = db.getUserDetail(Filters.UserName);
            //    }
            //if (!string.IsNullOrEmpty(Filters.ContactNo))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        ViewData[SELECTED_FILTED_Contacts] = db.getBrandDetail(Filters.ContactNo);
            //    }
            //if (!string.IsNullOrEmpty(Filters.Address))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.Address);
            //    }
            if (Request.IsAjaxRequest())
            {
                //return PartialView(TABLEDAILYUPATEVIEW, Filters);
                return (PartialViewResult)TableDeliveryUpateView(sort, sortdir, page, Filters);
            }
            return View();

        }


        public static string TABLEDELIVERYUPATEVIEW = "TableDeliveryUpateView";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public PartialViewResult TableDeliveryUpateView(string sort, string sortdir, int page = 1, DeliveryFilters Filters = null)
        {
            ViewBag.DeliveryUpateViewSort = sort;
            ViewBag.DeliveryUpateViewDir = sortdir;
            ViewBag.DeliveryUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + DELIVERY_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = DELIVERY_TITLE;
            return PartialView(TABLEDELIVERYUPATEVIEW, Filters);
        }
    }
}
