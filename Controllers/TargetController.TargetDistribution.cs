﻿using BootstrapHtmlHelper;
using BusinessExcel.Models;
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
    public partial class TargetController : Controller
    {
        public static string TARGETDISTRIBUTION = "TargetDistribution";
        public static string _TARGETDISTRIBUTIONASSIGN = "_TargetDistributionAssign";
        public static string TARGETDISTRIBUTION_TITLE = "Target Distribution";

        public static string _GETLOCATIONALOCATIONTITLE = "Assign to user";
        public static string _GETLOCATIONALOCATION = "_GetLocationAlocation";
        public ActionResult TargetDistribution()
        {
            BaseTarget target = new BaseTarget(true);
            if (!Roles.RoleExists("System Administrator")) Roles.CreateRole("System Administrator");
            if (!Roles.GetRolesForUser().Contains("System Administrator") && WebSecurity.CurrentUserName == "sujithvakkom@gmail.com")
                Roles.AddUserToRole(WebSecurity.CurrentUserName, "System Administrator");

            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + TARGETTEMPLATE;
            using (var db = new UsersContext())
            {
                try
                {
                    Session[Index.USER_PROFILE_INDEX] = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;
                }
                catch (Exception)
                {
                    WebSecurity.Logout();
                    RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
                }
            }
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            if (Request.IsAjaxRequest())
                return PartialView(TARGETDISTRIBUTION, target);
            return View(target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _GetLocationAlocation(BaseTarget target) 
        {
            List<LineTarget> lineTargets = new List<LineTarget>();
            using (var db = new SalesManageDataContext())
            {
                try
                {
                    int? userId = null;
                    try { userId = db.getUserID(target.UserName); }
                    catch (Exception) { }
                    lineTargets = db.getTargetTempletLineDetails(int.Parse(target.TargetTemplate),userID:userId);
                }
                catch (Exception) { }
            }
            target.LineTargets = lineTargets.ToList();
            return PartialView(target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TargetDistribution(BaseTarget target)
        {
            if (ModelState.ContainsKey("StartDate"))
                ModelState["StartDate"].Errors.Clear();
            if (ModelState.ContainsKey("EndDate"))
                ModelState["EndDate"].Errors.Clear();
            if (ModelState.ContainsKey("Description"))
                ModelState["Description"].Errors.Clear();
            var errorList = ModelState.GetErrors();
            using (var db = new SalesManageDataContext())
            foreach (var cat in target.LineTargets)
            {
                    try
                    {
                        cat.Catogery = db.getCategoryDetails(cat.Catogery).category_id.ToString();
                    }
                    catch (Exception) { }
            }
            string Message = "";
            int result = -1;
            if (ModelState.IsValid)
            {
                try
                {
                    result = target.Save(out Message);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Message;
                }
                using (var db = new SalesManageDataContext())
                {
                    try
                    {
                        int? userId = null;
                        try { userId = db.getUserID(target.UserName); }
                        catch (Exception) { }
                        target.LineTargets = db.getTargetTempletLineDetails(int.Parse(target.TargetTemplate), userID: userId).ToList();
                    }
                    catch (Exception) { }
                }
                ViewBag.Result = result;
                if (!string.IsNullOrEmpty(Message))
                    ViewBag.Message = Message;
                return PartialView(_GETLOCATIONALOCATION, target);
            }
            else
            {
                ViewBag.ModelErrors = ViewData.ModelState.GetErrors();
            }

            if (!Roles.RoleExists("System Administrator")) Roles.CreateRole("System Administrator");
            if (!Roles.GetRolesForUser().Contains("System Administrator") && WebSecurity.CurrentUserName == "sujithvakkom@gmail.com")
                Roles.AddUserToRole(WebSecurity.CurrentUserName, "System Administrator");

            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + TARGETTEMPLATE;
            using (var db = new UsersContext())
            {
                try
                {
                    Session[Index.USER_PROFILE_INDEX] = db.UserProfile.SingleOrDefault(x => x.UserName == User.Identity.Name).UserFullName;
                }
                catch (Exception)
                {
                    WebSecurity.Logout();
                    RedirectToAction(PublicController.WELCOME, PublicController.PUBLIC);
                }
            }
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            if (Request.IsAjaxRequest())
                return PartialView(_TARGETDISTRIBUTIONASSIGN, target);
            return View(target);

        }
    }
}