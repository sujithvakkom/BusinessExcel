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
    public class RosterController : Controller
    {
        //Roster
        public static string ROSTERTABLEPARTIAL = "RosterTablePartial";
        public static string ROSTERCONTROLLER = "Roster";
        public static string ROSTER_ACTION = "RosterIndex";
        public static string ROSTER_TITLE = "Roster";
        public static string AJAXCREATEROSTER = "AjaxCreateRoster";
        public static string ROSTERCREATIONMESSAGE = "RosterCreationMessage";
        public static string AJAXREMOVEROSTER = "AjaxRemoveRoster";


        public static string RosterActions_TITLE = "Roster List";
        public static string ROSTER_REPORTCONTROLLER = "Report";
        public static string ROSTER_ACTIONS = "RosterActions";

        
        public static string SELECTED_FILTED_USER = "SelectedFilteredUser";
     
        public static string SELECTED_FILTED_LOCATION = "SelectedFilteredLocation";

        // GET: /Admin/UserManagement



        //[Authorize(Roles = "Manager")]
        [Authorize(Roles = "System Administrator")]
        [HttpGet]
        public ActionResult RosterIndex(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ROSTER_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ROSTER_TITLE;

            //ViewBag.DailyUpateViewPage = page;
            //ViewBag.DailyUpateViewSort = sort;
            //ViewBag.DailyUpateViewDir = sortdir;

            if (Request.IsAjaxRequest()) return PartialView();
            return View();



        }

        [HttpPost]
        // [Authorize(Roles = "manager")]
        [Authorize(Roles = "System Administrator")]
        public PartialViewResult AjaxCreateRoster(Roster Roster, ActionViewFilters Filters = null)
        {

            ViewData.Add(ROSTERCREATIONMESSAGE, "");
            if (ModelState.IsValid)
            {

                using (var db = new SalesManageDataContext())
                {
                    db.Roster.Add(Roster);
                    db.SaveChanges();
                   
                }



                //if (!Roles.RoleExists(RoleName.RolesName))
                //{
                //    Roles.CreateRole(RoleName.RolesName);
                //    ViewData[ROLECREATIONMESSAGE] = "Role Created.";
                //    RoleName.RolesName = "";
                //}
                //else
                //{
                //    ModelState.AddModelError("RoleName", "Role existing.");
                //    ViewData[ROLECREATIONMESSAGE] = "Role existing.";
                //}
            }
            return PartialView(Roster);
        }

        // [Authorize(Roles = "Manager")]
        [Authorize(Roles = "System Administrator")]
        public PartialViewResult RosterTablePartial()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "System Administrator")]
        public String AjaxRemoveRoster(Roster Roster)
        {
            //ViewData.Add(ROLECREATIONMESSAGE, "");
            if (ModelState.IsValid)
            {
                //if (Roles.RoleExists(Roster.roster_id))
                //{
                //    var users = Roles.GetUsersInRole(RoleName.RolesName);
                //    if (users != null && users.Count() > 0)
                //        Roles.RemoveUsersFromRole(
                //            Roles.GetUsersInRole(RoleName.RolesName),
                //            RoleName.RolesName);
                //    Roles.DeleteRole(RoleName.RolesName);
                //    ViewData[ROLECREATIONMESSAGE] = "Role Removed.";
                //    RoleName.RolesName = "";
                //}
                //else
                //{
                //    ModelState.AddModelError("RoleName", "Role existing.");
                //    ViewData[ROLECREATIONMESSAGE] = "Role existing.";
                //}
            }
            return ViewData[ROSTERCREATIONMESSAGE].ToString();
        }


        public ActionResult RosterActions(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.DailyUpateViewSort = sort;
            ViewBag.DailyUpateViewDir = sortdir;
            ViewBag.DailyUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + RosterActions_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            //ViewBag.Title = ACTIONS_TITLE;

            if (!string.IsNullOrEmpty(Filters.UserName))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(Filters.UserName);
                }

            if (!string.IsNullOrEmpty(Filters.Location))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.Location);
                }
            //if (Request.IsAjaxRequest())
            //{
            //    //return PartialView(TABLEDAILYUPATEVIEW, Filters);
            //    return (PartialViewResult)TableDailyUpateView(sort, sortdir, page, Filters);
            //}
            return View();
        }

        //
        // GET: /Report/
        public static string TABLEROSTERUPATEVIEW = "TableRosterUpateView";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public PartialViewResult TableRosterUpateView(string sort, string sortdir, int page = 1, ActionViewFilters Filters = null)
        {
            ViewBag.RosterUpateViewSort = sort;
            ViewBag.RosterUpateViewDir = sortdir;
            ViewBag.RosterUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ROSTER_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = ROSTER_TITLE;
            return PartialView(TABLEROSTERUPATEVIEW, Filters);
        }

        //public PartialViewResult TableRosterUpateView(int page = 1)
        //{
        //    ViewBag.RosterUpateViewPage = page;
        //    ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + ROSTER_TITLE;
        //    ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
        //    ViewBag.Title = ROSTER_TITLE;
        //    return PartialView(TABLEROSTERUPATEVIEW);
        //}

    }
}
