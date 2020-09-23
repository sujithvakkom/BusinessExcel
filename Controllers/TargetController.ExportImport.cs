using BootstrapHtmlHelper;
using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace BusinessExcel.Controllers
{
    public partial class TargetController : Controller
    {

        public static string TARGET_IMPORT_TITLE = "Target Import / Export";

        public static string LOCATIONTARGETIMPORT = "LocationTargetImport";
        public static string _VIEWLOCATIONTARGETIMPORT = "_ViewLocationTargetImport";

        [HttpGet]
        public ActionResult LocationTargetImport()
        {
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
                return PartialView(LOCATIONTARGETIMPORT);
            return View();
        }

        public static string _BACKUPTARGET = "_BackupTarget";
        [HttpGet]
        public ActionResult _BackupTarget(BackupTargetFilter Filter)
        {
            return PartialView(Filter);
        }


        public static string BACKUP = "Backup";
        [HttpGet]
        public ActionResult Backup(BackupTargetFilter Filter)
        {
            if (!string.IsNullOrEmpty(Filter.Month))
            {

                using (var db = new SalesManageDataContext())
                {
                    var gv = new System.Web.UI.WebControls.GridView();
                    var result = db.GetTargetExport(Filter);
                    gv.DataSource = result;
                    gv.DataBind();

                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";
                    StringWriter objStringWriter = new StringWriter();
                    System.Web.UI.HtmlTextWriter objHtmlTextWriter = new System.Web.UI.HtmlTextWriter(objStringWriter);
                    gv.RenderControl(objHtmlTextWriter);
                    Response.Output.Write(objStringWriter.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View(LOCATIONTARGETIMPORT, Filter);
        }

        public static string _IMPORTDATA = "_ImportData";
        public PartialViewResult _ImportData() {
            return PartialView();
        }
    }
}