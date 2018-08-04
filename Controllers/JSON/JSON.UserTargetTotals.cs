using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace BusinessExcel.Controllers.JSON
{
    public partial class JSONController : Controller
    {
        
        //public JsonResult UserTargetTotals(int user_id,int target_id)
        //{
        //    JsonResult res = null;
        //    using (var db = new SalesManageDataContext()) {
         
        //        var items = db.getUsertargetTotalDetails(user_id, target_id);
        //        JSONPagininationModel<TargetTotalView> model = new JSONPagininationModel<TargetTotalView>();
           
        //        res = Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    return res;
        //}

    }
}
