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
      

        //Roster
        public static string ROSTERTABLEPARTIAL = "RosterTablePartial";
        public static string USERROSTER = "UserRoster";
        public static string USERROSTER_TITLE = "User Roster";
        public static string AJAXCREATEROSTER = "AjaxCreateRoster";
        public static string ROSTERCREATIONMESSAGE = "RosterCreationMessage";
        public static string AJAXREMOVEROSTER= "AjaxRemoveRoster";

        // GET: /Admin/UserManagement



        //[Authorize(Roles = "Manager")]
        [Authorize(Roles = "System Administrator")]
        [HttpGet]
        public ActionResult UserRoster()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERROSTER_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = USERROSTER_TITLE;
            return View();
        }

        [HttpPost]
        // [Authorize(Roles = "manager")]
        [Authorize(Roles = "System Administrator")]
        public PartialViewResult AjaxCreateRoster( RosterModel Roster)
        {

            ViewData.Add(ROSTERCREATIONMESSAGE, "");
            if (ModelState.IsValid)
            {
                RosterContext d = new RosterContext();
              
                 d.UserRoster.Add(Roster);
                 d.SaveChanges();
                
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
        public String AjaxRemoveRoster(RosterModel Roster)
        {
            ViewData.Add(ROLECREATIONMESSAGE, "");
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

    }
}
