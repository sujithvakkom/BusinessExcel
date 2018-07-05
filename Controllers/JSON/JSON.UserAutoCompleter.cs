using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BusinessExcel.Controllers.JSON
{
    public partial class JSONController : Controller
    {
        public static string USERAUTOCOMPLETER = "UserAutoCompleter";
        //public JsonResult UserAutoCompleter(String Search, int Page, ICollection<string> Extra)
        public JsonResult UserAutoCompleter(String Search, int Page, string Extra)
        {
            JsonResult res = null;

            var extras = ParseJSONString(Extra);

            int? locationId = null;

            try { locationId = int.Parse(((IDictionary)extras)["Location"].ToString()); }
            catch (Exception) { }

            using (var db = new SalesManageDataContext()) {
                int temp;

                var items = db.getUserDetails(search: Search, Page: Page,RowCount:out temp);
                JSONPagininationModel<UserDetail> model = new JSONPagininationModel<UserDetail>();
                model.CountPerPage = 20;
                model.OutputList = items;
                model.Count = temp;
                res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
