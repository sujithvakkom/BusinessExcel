using BusinessExcel.Models;
using BusinessExcel.Providers;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel.Controllers
{
    [Authorize(Roles = BERoleDetails.SYSTEM_ADMINISTRATOR)]
    public partial class AdminController : Controller
    {
 

        public static string USERSLIST = "UserList";
        public static string USERSLIST_TITLE = "User List";
        public static string SELECTED_FILTED_USER = "SelectedFilteredUser";
        public static string SELECTED_FILTED_USER_FIRST_NAME = "SelectedFilteredUserFirstName";




        // GET: /Admin/UserManagement

        [Authorize(Roles = "System Administrator")]
        [HttpGet]
        public ActionResult UserList(string sort, string sortdir, int page = 1, UserViewFilters Filters = null)
        {
            //ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERMANAGEMENT_TITLE;
            //ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            //ViewBag.Title = USERMANAGEMENT_TITLE;
            //if (Request.IsAjaxRequest())
            //    return PartialView();



            ViewBag.UserUpateViewSort = sort;
            ViewBag.UserUpateViewDir = sortdir;
            ViewBag.UserUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERSLIST_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            //ViewBag.Title = ACTIONS_TITLE;

            if (!string.IsNullOrEmpty(Filters.user_name))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(Filters.user_name);
                }

            if (!string.IsNullOrEmpty(Filters.first_name))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER_FIRST_NAME] = db.getUserDetailByName(Filters.first_name);
                }

            //if (!string.IsNullOrEmpty(Filters.LocationID))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.LocationID);
            //    }

            //if (!string.IsNullOrEmpty(Filters.TargetID))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        ViewData[SELECTED_FILTED_TARTET] = db.getTargetDetail(Filters.TargetID);
            //    }



            //if (Request.IsAjaxRequest())
            //{
                //return PartialView(TABLEDAILYUPATEVIEW, Filters);
                return (PartialViewResult)UserUpateView(sort, sortdir, page, Filters);
            //}
            //return View(Filters);




          
        }
        public static string USERUPDATEVIEW = "UserList";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public PartialViewResult UserUpateView(string sort, string sortdir, int page = 1, UserViewFilters Filters = null)
        {
            ViewBag.UserUpateViewSort = sort;
            ViewBag.UserUpateViewDir = sortdir;
            ViewBag.UserUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERSLIST_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = USERSLIST_TITLE;
            return PartialView(USERUPDATEVIEW, Filters);
        }



    }
}
