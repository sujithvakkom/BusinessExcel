using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
        public static string TARGETACHIEVEMENT_ACTIONS_TITLE = "Target & Achievement (Roster)";

        public static string USER_TARGET_ACHIEVEMENT_TITLE = "Target & Achievement (User) ";
        public static string USER_TARGET_ACHIEVEMENT_PAGE_TITLE = "Target & Achievement";
        public static string USER_TARGET_ACHIEVEMENT_DETAILS = "UserTargetAchievementDetails";

        public static string USER_TARGET_ACHIEVEMENT_ACTION = "UserTargertAchievement";

        public static string USER_TARGET_UPDATE_ACHIEVEMENT = "UpdateAcheivedAmt";

        public static string USER_TARGET_UPDATED_TOTAL = "getUpdatedTargetTotal";
        public static string LOAD_USER_TARGET_UPDATED_TOTAL = "LoadUpdatedTargetTotal";

        // public static string REPORTCONTROLLER = "Report";
        public static string TARGETACHIEVEMENTACTIONS = "TargetAchievementActions";
        public static string USER_TARGET_DETAILS = "UserTargetDetails";
        public static string USER_TARGET_DETAILS_ACTION = "UserTargetDetailsAction";

       // 
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

        public static string EXPORTEXCEL_TARGETACHIEVEMENT = "ExportExcel_TargetAchievement";
        // public static string EXPORTEXCEL_TITLE = "Export Excel";
        //public ActionResult ExportExcel_TargetAchievement(ActionViewFilters Filters = null)
        //{

        //    using (var db = new SalesManageDataContext())
        //    {
        //        var gv = new System.Web.UI.WebControls.GridView();
        //        gv.DataSource = db.GetTargetAchievementViewPagingExport(Filters);
        //        gv.DataBind();
        //        Response.ClearContent();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment; filename= TargetAchievement.xls");
        //        Response.ContentType = "application/ms-excel";
        //        Response.Write("<style> TD { mso-number-format:\\@; } </style>");
        //        Response.Charset = "";
        //        StringWriter objStringWriter = new StringWriter();
        //        System.Web.UI.HtmlTextWriter objHtmlTextWriter = new System.Web.UI.HtmlTextWriter(objStringWriter);
        //        gv.RenderControl(objHtmlTextWriter);
        //        Response.Output.Write(objStringWriter.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //    return View("Index");
        //}

        ////[Authorize(Roles = "Manager")]
        //[Authorize(Roles = "System Administrator")]
        //[HttpGet]
        //public ActionResult UserTargertAchievement(string sort, string sortdir, int page = 1, UserTargetDetailsView Filters = null)
        //{
        //    Filters.UserID = 228;

        //    ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USER_TARGET_ACHIEVEMENT_TITLE;
        //    ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
        //    ViewBag.Title = USER_TARGET_ACHIEVEMENT_TITLE;

        //    ViewBag.UserTargetViewPage = page;
        //    ViewBag.UserTargetViewSort = sort;
        //    ViewBag.UserTargetViewDir = sortdir;

        //    if (Request.IsAjaxRequest()) return PartialView();
        //    return View(new UserTargetDetailsView());
        //}

        public static string USERTARGET_ACHIEVEMENT_VIEW = "USerTargetAchievementView";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public ActionResult UserTargetAchievementView(string sort, string sortdir, int page = 1, TargetAchievementView target = null)
        {
            ViewBag.UserTargetViewSort = sort;
            ViewBag.UserTargetViewDir = sortdir;
            ViewBag.UserTargetViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USER_TARGET_ACHIEVEMENT_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = USER_TARGET_ACHIEVEMENT_TITLE;
            if (target.target_id > 0)
            {
                target.TargetTotal =  LoadTargetTotal(target.user_id.Value, target.target_id.Value);
            }
            return PartialView(USERTARGET_ACHIEVEMENT_VIEW,target);
        }

        //public ActionResult UserTargetAchievementDetails(string sort, string sortdir, int page = 1, TargetAchievementView Filters)
        //{


        //    if (Request.IsAjaxRequest())
        //    {
        //        //return PartialView(TABLEDAILYUPATEVIEW, Filters);
        //        return (PartialViewResult)UserTargetAchievementView(sort, sortdir, page,Filters);
        //    }
        //    return View();
        //}


        //Index Action
        [HttpGet]
        // [Authorize(Roles = "manager")]
        [Authorize(Roles = "System Administrator")]
        public ActionResult UserTargetDetails(UserTargetDetailsView usr)
        {

            //getUserTargetsNextPrvsThreeMonths(291);

            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USER_TARGET_ACHIEVEMENT_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = USER_TARGET_ACHIEVEMENT_TITLE;


            if(usr.start_date ==null)
            {
              
                usr.start_date = DateTime.Now.Date;
                usr.end_date = DateTime.Now.Date;
            }
            if (string.IsNullOrEmpty(usr.UserName))
            {
                usr.UserID = 291;
            }


            using (var db = new SalesManageDataContext())
            {
                usr = db.getUserTargetDetails(usr);
              
                string d = usr.Full_Name;

                if (!string.IsNullOrEmpty(usr.UserName))
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserDetail(usr.UserName);

                  
                }
            }
          
            if (Request.IsAjaxRequest()) return PartialView(usr);
            return View(usr);

          
        }
       
        //public List<UserTargetDetailsView> UserData(UserTargetDetailsView usr)
        //{
           

            //using (var db = new SalesManageDataContext())
            //{
            //    return db.getUserTargetDetails(usr);
            //}

            //if (usr != null)
            //{
            //    if (!string.IsNullOrEmpty(usr.UserName))
            //        using (var db = new SalesManageDataContext())
            //        {
            //            ViewData[SELECTED_FILTED_USER] = db.getUserDetail(usr.UserName);
            //        }

            //    if (usr.Location_Id > 0)
            //        using (var db = new SalesManageDataContext())
            //        {
            //            ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(usr.Location_Id.ToString());
            //        }

            //    if (usr.start_date != default(DateTime))
            //    {
            //        usr.Quarter_Name = GetQuarter(usr.start_date.Value);
            //        usr.Type = targetType.WM.ToString();
            //        usr.Account = GEtAccountName(usr.Location_Name);
            //        usr.QuarterMonths = GetMonths(usr.start_date.Value);
                   
            //        using (var db = new SalesManageDataContext())
            //        {
            //            if (usr.SuperVisorId > 0)
            //                usr.FieldMan = db.getUserFullNameByID(usr.SuperVisorId);

            //            usr.SalesMan = db.getUserFullNameByID( db.getParent(usr.UserID)  );
            //        }
            //    }
            //}
            //else
            //{
            //    usr = new UserTargetDetailsView();
            //    usr.Account = "";
            //    usr.FieldMan = "";
            //    usr.Full_Name = "";
            //    usr.Location_Id = 0;
            //    usr.Location_Name = "";
            //    usr.QuarterMonths = new string[0].ToList();
            //    usr.Quarter_Name = "";
            //    usr.SalesMan = "";
            //    usr.start_date = default(DateTime);
            //    usr.end_date = default(DateTime);
            //    usr.SuperVisorId = 0;
            //    usr.Type= targetType.WM.ToString();
            //    usr.UserID = 0;
            //    usr.UserName = "";
            //}

              
            //return usr;
           
           
       // }

        //private string GetQuarter(DateTime date)
        //{
        //    if (date.Month >= 1 && date.Month <= 3)
        //        return "Q1 - " + date.Year;
        //    else if (date.Month >= 4 && date.Month <= 6)
        //        return "Q2 - " + date.Year;
        //    else if (date.Month >= 7 && date.Month <= 9)
        //        return "Q3 - " + date.Year;
        //    else
        //        return "Q4 - " + date.Year;
        //}

        //private  string GetQuarter(DateTime date)
        //{
        //    if (date.Month >= 4 && date.Month <= 6)
        //        return "Q1 - "+date.Year;
        //    else if (date.Month >= 7 && date.Month <= 9)
        //        return "Q2 - " + date.Year;
        //    else if (date.Month >= 10 && date.Month <= 12)
        //        return "Q3 - " + date.Year;
        //    else
        //        return "Q4 - " + date.Year;
        //}

        //private List<string> GetMonths(DateTime date)
        //{
        //    string[] months = new string[3];            // Declare int array of zeros


        //    if (date.Month >= 1 && date.Month <= 3)
        //    {
        //        months[0] = "JAN";
        //        months[1] = "FEB";
        //        months[2] = "MAR";

        //    }
        //    else if (date.Month >= 4 && date.Month <= 6)
        //    {
        //        months[0] = "APR";
        //        months[1] = "MAY";
        //        months[2] = "JUN";

        //    }
        //    else if (date.Month >= 7 && date.Month <= 9)
        //    {
        //        months[0] = "JUL";
        //        months[1] = "AUG";
        //        months[2] = "SEP";
        //    }
        //    else
        //    {
        //        months[0] ="OCT";
        //        months[1] ="NOV";
        //        months[2] = "DEC";
        //    }


        //    return months.ToList();
        //}
        //public JsonResult UpdateAcheivedAmt(TargetAchievementView targetModel)

        //{

        //    // Update model to your db
        //    var  result = 0;

        //    using (var db = new SalesManageDataContext())
        //    {
        //        if (db.UpdateTargetAchievementAmt(targetModel) > 0)
        //        {
        //            result = 1;
        //        }
        //        else
        //        {
        //            result = 0;
        //        }

        //    }

               

        //    return Json(result, JsonRequestBehavior.AllowGet);

        //}


        /// <summary>
        /// Return user target full details.
        /// Returns null if user not assigned to any location within given period
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public JsonResult getUserTargetsNextPrvsThreeMonths(int user_id)
        {
            UserTargetDetailsView usrDet = null;
            using (var db = new SalesManageDataContext())
            {
                usrDet = db.getUserTargets(user_id);
            }
                return Json(usrDet, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getUpdatedTargetTotal(TargetAchievementView targetModel)
        {

            List<TargetTotalView> items = null;
            using (var db = new SalesManageDataContext())
            {


                if (db.UpdateTargetAchievementAmt(targetModel) > 0)
                {
                    items = db.getUsertargetTotalDetails(targetModel.user_id.Value, targetModel.target_id.Value);

                }
              
              
            }

            return Json(items, JsonRequestBehavior.AllowGet);

        }

        public List<TargetTotalView> LoadTargetTotal(int user_id,int target_id)
        {

       

      //      res = res.Where(x => month <= x.start_date.Value.Month && month >= x.start_date.Value.Month);



            List<TargetTotalView> items = null;
            using (var db = new SalesManageDataContext())
            {


                items = db.getUsertargetTotalDetails(user_id, target_id).ToList();


            }

            return items;

        }
        public JsonResult LoadUpdatedTargetTotal(TargetAchievementView targetModel)
        {


            List<TargetTotalView> items = null;
            using (var db = new SalesManageDataContext())
            {


                    items = db.getUsertargetTotalDetails(targetModel.user_id.Value, targetModel.target_id.Value);


            }

            return Json(items, JsonRequestBehavior.AllowGet);

        }

        private string GEtAccountName(string location)
        {
            string acc = string.Empty;

            acc = location.Split(':').FirstOrDefault();

            return acc;
        }

        private enum targetType
        {
            None,
            WM,
            WC
            
        }
    }
}