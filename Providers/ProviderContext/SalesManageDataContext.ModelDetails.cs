
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

using BusinessExcel.Extentions;
using BusinessExcel.Models;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public virtual ItemModel getModelDetails(string serarch)
        {
            if (!String.IsNullOrEmpty(serarch))
            {
                var item_code = serarch != null ?
                    new SqlParameter("@filter", serarch) :
                    new SqlParameter("@filter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

                int? page = null;
                var page_size = page != null ?
                    new SqlParameter("@page_size", page) :
                    new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

                int? page_num = null;
                var page_number = page_num != null ?
                    new SqlParameter("@page_number", page_num) :
                    new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

                int? row = null;
                var row_count = row != null ?
                    new SqlParameter("@row_count", row) :
                    new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
                row_count.Direction = System.Data.ParameterDirection.Output;

                var items = this.Database.SqlQuery<ItemModel>(
                                                "[sc_salesmanage_merchant].[getModelDetails]  @item_code ,@page_number ,@page_size ,@row_count OUTPUT", item_code, page_number, page_size, row_count)
                                                .ToList();

                return items[0];
            }
            return null;
        }


        public virtual List<ItemModel> getModelDetails(string search, int Page, out int RowCount, string UserName=null)
        {
            List<ItemModel> items = new List<ItemModel>();

            var item_codePar = search != null ?
                new SqlParameter("@filter", search) :
                new SqlParameter("@filter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;
            //db.getItemDetailsImport(null, null, null);
            try
            {
                items = this.Database.SqlQuery<ItemModel>(
                                                "[sc_salesmanage_merchant].[getModelDetails]  @filter ,@page_number ,@page_size ,@row_count OUTPUT", 
                                                item_codePar, 
                                                page_number, 
                                                page_size, 
                                                row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return items;
        }
    }
}
