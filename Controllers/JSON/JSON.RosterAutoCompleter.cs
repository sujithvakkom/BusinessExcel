using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BusinessExcel.Controllers.JSON
{
    public partial class JSONController : Controller
    {

        public static string ROSTERAUTOCOMPLETER = "RosterAutoCompleter";
        public JsonResult RosterAutoCompleter(String Search, int Page, string Extra)
        {
            JsonResult res = null;
        

            var extras = ParseJSONString(Extra);

            int? locationId = null;

            try { locationId = int.Parse(((IDictionary)extras)["location_id"].ToString()); }
            catch (Exception) { }

            using (var db = new SalesManageDataContext())
            {
                int temp;

                var items = db.getRosterDetails( search: Search, Page: Page, RowCount: out temp);
                JSONPagininationModel<RosterViewModel> model = new JSONPagininationModel<RosterViewModel>();
                model.CountPerPage = 20;
                model.OutputList = items;
                model.Count = temp;
                res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }
    }
}