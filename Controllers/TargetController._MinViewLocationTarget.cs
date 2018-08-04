using BootstrapHtmlHelper;
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
        public static string _MINVIEWLOCATIONTARGET = "_MinViewLocationTarget";
        //_LocationTargetEditCreate

        [HttpPost]
        public PartialViewResult _MinViewLocationTarget(BaseTarget target)
        {
            List<LineTarget> lineTargets = new List<LineTarget>();
            using (var db = new SalesManageDataContext())
            {
                try
                {
                    int? userId = null;
                    try { userId = db.getUserID(target.UserName); }
                    catch (Exception) { }
                    lineTargets = db.getTargetTempletLineDetails(int.Parse(target.TargetTemplate), userID: userId);
                }
                catch (Exception) { }
            }
            target.LineTargets = lineTargets.ToArray();
            return PartialView(target);
        }
    }
}