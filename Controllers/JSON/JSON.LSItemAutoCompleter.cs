using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Providers.Rest;
using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web.Mvc;

namespace BusinessExcel.Controllers.JSON
{
    public partial class JSONController : Controller
    {
        public static string LSITEMAUTOCOMPLETER = "LSItemAutoCompleter";
        public JsonResult LSItemAutoCompleter(String Search, int Page, string Extra)
        {
            bool extendedSearch = false;
            try {
                var extras = ParseJSONString(Extra);
                extendedSearch = bool.Parse(((IDictionary)extras)["exteded_search"].ToString());
            }
            catch (Exception) {
            }

            MyRestCleint cleint = new MyRestCleint(ConfigurationManager.AppSettings["LSDataAPIBaseURL"]);


            JSONPagininationModel<ItemModel> model = new JSONPagininationModel<ItemModel>();
            model.CountPerPage = 20;
            model.OutputList = cleint.AllProducts(Search, Page, extendedSearch);
            model.Count = 100;
            var res = Json(model, JsonRequestBehavior.AllowGet);
            return res;
        }
    }
}