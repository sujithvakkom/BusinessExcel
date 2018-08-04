using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace BusinessExcel.Controllers.JSON
{
    public partial class JSONController : Controller
    {
        public static string TARGETAUTOCOMPLETER = "TargetAutoCompleter";
        public JsonResult TargetAutoCompleter(String Search,int Page, string Extra)
        {
            JsonResult res = null;

            var extras = this.ParseJSONString(Extra); 

            int? locationId = null;

            try { locationId = int.Parse(((IDictionary)extras)["Location"].ToString()); }
            catch (Exception) { }

            using (var db = new SalesManageDataContext()) {
                int temp;

                var items = db.getTargetDetails(search: Search, PageNum: Page,PageSize:null,RowCount:out temp);
                JSONPagininationModel<TargetMasterDetails> model = new JSONPagininationModel<TargetMasterDetails>();
                model.CountPerPage = 20;
                model.OutputList = items;
                model.Count = temp;
                res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
