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
        //
        // GET: /JSON/

        public static string ITEMAUTOCOMPLETER = "ItemAutoCompleter";
        public static string JSONCONTROLLER = "JSON";
        public JsonResult ItemAutoCompleter(String Search,int Page)
        {
            JsonResult res = null;
            using (var db = new SalesManageDataContext()) {


                var item_codePar = Search != null ?
                    new SqlParameter("@item_code", Search) :
                    new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar);
                item_codePar.Value = DBNull.Value;

                int? page = null;
                var page_size = page != null ?
                    new SqlParameter("@page_size", Search) :
                    new SqlParameter("@page_size", System.Data.SqlDbType.BigInt);
                page_size.Value = DBNull.Value;

                int? page_num = null;
                var page_number = page != null ?
                    new SqlParameter("@page_number", Search) :
                    new SqlParameter("@page_number", System.Data.SqlDbType.BigInt);
                page_size.Value = DBNull.Value;

                int? row = null;
                var row_count = row != null ?
                    new SqlParameter("@row_count", Search) :
                    new SqlParameter("@row_count", System.Data.SqlDbType.BigInt);
                row_count.Value = DBNull.Value;
                row_count.Direction = System.Data.ParameterDirection.Output;

                //db.getItemDetailsImport(null, null, null);

                var items = db.Database.SqlQuery<ItemDetails>(
                                                "[sc_salesmanage_merchant].[getItemDetails]  @item_code ,@page_number ,@page_size ,@row_count OUTPUT", item_codePar,page_size,row_count)
                                                .ToList();

                /*
                var item_code = new SqlParameter("@item_code", "");
                //
                //calling stored procedure to get paged data.
                var items = db.Database.SqlQuery<ItemDetails>(
                                                "[sc_salesmanage_merchant].[getItemDetailsTemp] @item_code", item_code)
                                                .ToList();
                                                */
                res = Json(items, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
