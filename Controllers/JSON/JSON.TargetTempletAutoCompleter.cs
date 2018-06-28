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
        public static string TARGETTEMPLETAUTOCOMPLETER = "TargetTempletAutoCompleter";
        public JsonResult TargetTempletAutoCompleter(String Search,int Page)
        {
            JsonResult res = null;
            using (var db = new SalesManageDataContext()) {
                int temp;

                var items = db.getTargetTempletDetails(search: Search, Page: Page,RowCount:out temp);
                JSONPagininationModel<TargetTemplet> model = new JSONPagininationModel<TargetTemplet>();
                model.CountPerPage = 20;
                model.OutputList = items;
                model.Count = temp;
                res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
