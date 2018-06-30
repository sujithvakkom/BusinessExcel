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
        
        public static string CATEGORYAUTOCOMPLETER = "CategoryAutoCompleter";
        public JsonResult CategoryAutoCompleter(String Search,int Page)
        {
            JsonResult res = null;
            using (var db = new SalesManageDataContext()) {
                int temp;

                var items = db.getCategoryDetails(search: Search, Page: Page,RowCount:out temp);
                JSONPagininationModel<CategoryDetail> model = new JSONPagininationModel<CategoryDetail>();
                model.CountPerPage = 20;
                model.OutputList = items;
                model.Count = temp;
                res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
