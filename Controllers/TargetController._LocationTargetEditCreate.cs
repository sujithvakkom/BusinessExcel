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

        public static string _LOCATIONTARGETEDITCREATE = "_LocationTargetEditCreate";

        public static string _NEWLOCATIONTARGETEDITCREATE = "_NewLocationTargetEditCreate";
        //_LocationTargetEditCreate
        [HttpGet]
        public PartialViewResult _LocationTargetEditCreate(int TargetTempletID)
        {
            BaseTarget target = null;
            List<LineTarget> lineTargets = new List<LineTarget>();
            using (var db = new SalesManageDataContext())
            {
                try
                {
                    var x = db.getTargetTemplet(TargetTempletID);
                    lineTargets = db.getTargetTempletLineDetails(TargetTempletID, false);
                    target = (BaseTarget)x;
                    for (int i = lineTargets.Count - 1; i < 6; i++)
                        lineTargets.Add(new LineTarget());
                    target.LineTargets = lineTargets.ToArray();
                }
                catch (Exception) { }
            }
            return PartialView(target);
        }

        [HttpGet]
        public ActionResult _NewLocationTargetEditCreate()
        {
            var target = new BaseTarget(true);
            return PartialView(_LOCATIONTARGETEDITCREATE, target);
        }
    }
}