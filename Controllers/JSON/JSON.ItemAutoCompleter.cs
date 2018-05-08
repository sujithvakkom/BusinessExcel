using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace BusinessExcel.Controllers.JSON
{
    public partial class JSONController : Controller
    {
        //
        // GET: /JSON/

        public static string ITEMAUTOCOMPLETER = "ItemAutoCompleter";
        public static string JSONCONTROLLER = "JSON";
        public JsonResult ItemAutoCompleter(String Search,int Page)
        {
            JsonResult res = null;
            using (var db = new SalesManageDataContext()) {

                int temp = 0;
                var items= db.getItemDetails(Search, Page, out temp);

                JSONPagininationModel<ItemDetails> model = new JSONPagininationModel<ItemDetails>();
                model.CountPerPage = 20;
                model.OutputList = items;
                model.Count = temp;
                res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
