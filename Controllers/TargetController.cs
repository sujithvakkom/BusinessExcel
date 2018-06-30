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
    public partial class TargetController : Controller
    {
        //
        // GET: /Target/
        public static string TARGETCONTROLLER = "Target";
        public static string TARGET = "Target";
        public static string TARGET_EDIT = "EditRoster";
        public static string TARGET_TITLE = "Target";

        public static string AJAXCREATETARGETMASTER = "AjaxCreateTargetMaster";
        public static string AJAXCREATETARGETDETAILS = "AjaxCreateTargetDetails";
        public static string TARGETCREATIONMESSAGE = "TargetCreationMessage";
        public static string AJAXDELETETARGET = "AjaxDeleteTarget";
        public static string TABLETARGETUPATEVIEW = "TableTargetUpateView";
        public static string SELECTED_FILTED_CATERGORY = "SelectedFilteredTarget";
        public static string TARGELIST = "TargetList";
        public static string TARGETMASTERSAVE = "TargetMasterSave";
        public static string TARGEADDLIST = "TargetAddToList";
       

        public ActionResult Target(string sort, string sortdir, int page = 1, TargetViewFilters Filters = null)
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + TARGET_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = TARGET_TITLE;

            if (Request.IsAjaxRequest()) return PartialView();
            return View();

        }
        public PartialViewResult TableTargetUpateView(string sort, string sortdir, int page = 1, TargetViewFilters Filters = null)
        {
            ViewBag.RosterUpateViewSort = sort;
            ViewBag.RosterUpateViewDir = sortdir;
            ViewBag.RosterUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + TARGET_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = TARGET_TITLE;
            return PartialView(TABLETARGETUPATEVIEW, Filters);
        }
        [HttpPost]
        // [Authorize(Roles = "manager")]
        [Authorize(Roles = "System Administrator")]
        public PartialViewResult AjaxCreateTargetMaster(TargetMaster master)
        {


            //ViewData.Add(TARGETCREATIONMESSAGE, "");
            //if (ModelState.IsValid)
            //{


            //    using (var db = new SalesManageDataContext())
            //    {

            //        var data = db.Roster.Where(u => u.user_name == Roster.user_name && ((u.start_date <= Roster.end_date && u.end_date >= Roster.start_date)) && u.roster_id != Roster.roster_id).FirstOrDefault();

            //        var existdata = db.Roster.Where(u => u.roster_id == Roster.roster_id).FirstOrDefault();



            //        if (data == null)
            //        {
            //            if (Roster.roster_id <= 0)
            //            {
            //                //  db.Roster.Add(Roster);

            //                db.InsertUpdateRoster(Roster, true);
            //            }
            //            else
            //            {

            //                if (existdata != null)
            //                {
            //                    //db.Entry(existdata).CurrentValues.SetValues(Roster);
            //                    db.InsertUpdateRoster(Roster, false);

            //                }
            //            }


            //            // db.SaveChanges();

            //            ModelState.Clear();



            //        }
            //        else
            //        {
            //            ViewData[ROSTERCREATIONMESSAGE] = "Given user (" + data.u_name + ") already assigned to '" + data.location_name + "' ( " + (data.start_date).Value.ToString("dd/MMM/yyyy") + " - " + data.end_date.Value.ToString("dd/MMM/yyyy") + ")";
            //            ModelState.AddModelError("RosterName", "Roster existing.");
            //        }


            //    }


                return PartialView(master);

            //}
            //else
            //{

            //    return PartialView(Roster);
            //}

        }

        [HttpPost]
        // [Authorize(Roles = "manager")]
        [Authorize(Roles = "System Administrator")]
        public PartialViewResult AjaxCreateTargetDetails(TargetMaster master)
        {


            //ViewData.Add(TARGETCREATIONMESSAGE, "");
            //if (ModelState.IsValid)
            //{


            //    using (var db = new SalesManageDataContext())
            //    {

            //        var data = db.Roster.Where(u => u.user_name == Roster.user_name && ((u.start_date <= Roster.end_date && u.end_date >= Roster.start_date)) && u.roster_id != Roster.roster_id).FirstOrDefault();

            //        var existdata = db.Roster.Where(u => u.roster_id == Roster.roster_id).FirstOrDefault();



            //        if (data == null)
            //        {
            //            if (Roster.roster_id <= 0)
            //            {
            //                //  db.Roster.Add(Roster);

            //                db.InsertUpdateRoster(Roster, true);
            //            }
            //            else
            //            {

            //                if (existdata != null)
            //                {
            //                    //db.Entry(existdata).CurrentValues.SetValues(Roster);
            //                    db.InsertUpdateRoster(Roster, false);

            //                }
            //            }


            //            // db.SaveChanges();

            //            ModelState.Clear();



            //        }
            //        else
            //        {
            //            ViewData[ROSTERCREATIONMESSAGE] = "Given user (" + data.u_name + ") already assigned to '" + data.location_name + "' ( " + (data.start_date).Value.ToString("dd/MMM/yyyy") + " - " + data.end_date.Value.ToString("dd/MMM/yyyy") + ")";
            //            ModelState.AddModelError("RosterName", "Roster existing.");
            //        }


            //    }


            return PartialView(master);

            //}
            //else
            //{

            //    return PartialView(Roster);
            //}

        }

        //
        // GET: /Author/
        //[NonAction]
        public static List<TargetDetail> TargetListD()
        {
            List<TargetDetail> users = new List<TargetDetail>()
            {
              new TargetDetail (){ Value=100,HasBonus=true },
              new TargetDetail (){ Value=600,HasBonus=true},
            };
            return users;
        }

      //  [HttpGet]
        public ActionResult TargetList()
        {

            TargetViewModel objstudentmodel = new TargetViewModel();
            objstudentmodel.TargetListsDetails = new List<TargetDetail>();

            //// List<TargetDetails> emp = new List<TargetDetails>();
            //// emp.Add(new TargetDetails { value = list.value, has_bonus = list.has_bonus, category_id = list.category_id, target_id = list.target_id, model_id = list.model_id, target_qty = list.target_qty, target_line_id = list.target_line_id,});

            //objstudentmodel.TargetListsDetails.Add(new TargetDetails { value = 100, has_bonus = true, category_id = 1, target_id = 1, model_id = 1, target_qty = 0, target_line_id = 1 });
            objstudentmodel.TargetListsDetails.Add(new TargetDetail { Value = 6002, HasBonus = true, CategoryID = 1, Achievement = 1, ModelID = 1, TargetID = 0, TargetLineID = 1 });
            return View(objstudentmodel);
        }

        public int SaveMaster(TargetMaster master)
        {
            int isSave = 0;

            if (ModelState.IsValid)
            {


                using (var db = new SalesManageDataContext())
                {
                    db.TargetMaster.Add(master);


                    //db.Entry(existdata).CurrentValues.SetValues(Roster);
                    //db.InsertUpdateRoster(Roster, false);


                  
                    db.SaveChanges();
                    isSave = master.TargetID;
                    ModelState.Clear();



                }
                    
                
            }

            return isSave;


            
        }


        public int SaveMDetails(List<TargetDetail> list)
        {
            int isSave = 0;

            ViewData.Add(TARGETCREATIONMESSAGE, "");


            using (var db = new SalesManageDataContext())
                {
                    db.TargetDetails.AddRange(list);


                    //db.Entry(existdata).CurrentValues.SetValues(Roster);
                    //db.InsertUpdateRoster(Roster, false);


                    isSave = db.SaveChanges();

                    ModelState.Clear();



                }

            if (isSave > 0)
                ViewData[TARGETCREATIONMESSAGE] = "TARGET SAVED SUCCCESSFULLY";
            else
                ViewData[TARGETCREATIONMESSAGE] = "ERROR....PLEASE CONTACT ADMIN";

            return isSave;



        }




        [HttpPost]
        public JsonResult TargetAddToList(List<TargetDetail> list)
        {
            int detId = SaveMDetails(list);
            return Json(detId, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult TargetMasterSave(TargetMaster master)
        {
            int masterId = SaveMaster(master);

            return Json(masterId, JsonRequestBehavior.AllowGet);
        }
    }
}
