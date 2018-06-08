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

using System.Data.Entity;
using System.IO;

namespace BusinessExcel.Controllers
{
    public class RosterController : Controller
    {
        //Roster
        public static string ROSTERTABLEPARTIAL = "RosterTablePartial";
        public static string ROSTERCONTROLLER = "Roster";
        public static string ROSTER_ACTION = "RosterIndex";
        public static string ROSTER_EDIT = "EditRoster";
        public static string ROSTER_TITLE = "Roster";
        public static string AJAXCREATEROSTER = "AjaxCreateRoster";
        public static string ROSTERCREATIONMESSAGE = "RosterCreationMessage";
        public static string AJAXDELETEROSTER = "AjaxDeleteRoster";


        public static string RosterActions_TITLE = "Roster List";
       
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

        public PartialViewResult EditRoster( Roster ros )
        {


            if (!string.IsNullOrEmpty(ros.user_name))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(ros.user_name);
                }

            if (!string.IsNullOrEmpty(ros.location_id))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(ros.location_id);
                }

            

            return  PartialView(AJAXCREATEROSTER, ros);
            //return Json(roster_Id, JsonRequestBehavior.AllowGet);
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

                 var data=   db.Roster.Where(u => u.user_name == Roster.user_name && ((u.start_date<=Roster.end_date && u.end_date >=Roster.start_date)  ) && u.roster_id != Roster.roster_id  ).FirstOrDefault();

                    var existdata = db.Roster.Where(u => u.roster_id == Roster.roster_id).FirstOrDefault();



                    if (data == null)
                    {
                        if (Roster.roster_id <= 0)
                        {
                            db.Roster.Add(Roster);
                        }
                        else
                        {

                            if (existdata != null)
                            {
                                db.Entry(existdata).CurrentValues.SetValues(Roster);
                            }
                        }

                        db.SaveChanges();

                        ModelState.Clear();

                       
                       
                    }
                    else
                    {
                        ViewData[ROSTERCREATIONMESSAGE] = "Given user ("+data.u_name+") already assigned to '" +data.location_name + "' ( "+(data.start_date).Value.ToString("dd/MMM/yyyy") + " - "+data.end_date.Value.ToString("dd/MMM/yyyy")+")";
                        ModelState.AddModelError("RosterName", "Roster existing.");
                    }

                       
                }

                
                return PartialView(new Roster());

            }
            else
            {
                return PartialView(Roster);
            }
          
        }

        // [Authorize(Roles = "Manager")]
        [Authorize(Roles = "System Administrator")]
        public PartialViewResult RosterTablePartial()
        {
            return PartialView();
        }

       
      

        public JsonResult AjaxDeleteRoster(int ID)
        {
            var deleteId = 0;
            if(ID > 0)
            {
                using (var db = new SalesManageDataContext())
                {
             

                    db.Roster.RemoveRange(db.Roster.Where(c => c.roster_id == ID));

                    deleteId = db.SaveChanges();

                }
            }
            return Json(deleteId, JsonRequestBehavior.AllowGet);
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

            if (!string.IsNullOrEmpty(Filters.LocationID))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.LocationID);
                }



           // ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.Location);

            //if ((Filters.StartDate))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        //ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.Location);
            //    }

            //if (!string.IsNullOrEmpty(Filters.EndDate))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        //ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.Location);
            //    }


            if (Request.IsAjaxRequest())
            {
                //return PartialView(TABLEDAILYUPATEVIEW, Filters);
                return (PartialViewResult)TableRosterUpateView(sort, sortdir, page, Filters);
            }
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

        public static string EXPORTEXCEL = "ExportExcel";
        public static string EXPORTEXCEL_TITLE = "Export Excel";
        public ActionResult ExportExcel(ActionViewFilters Filters = null)
        {

            using (var db = new SalesManageDataContext())
            {
                var gv = new System.Web.UI.WebControls.GridView();
                gv.DataSource = db.GetRosterUpateViewPagingExport(Filters);

                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=RosterList - "+DateTime.Now.Date+".xls");
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
