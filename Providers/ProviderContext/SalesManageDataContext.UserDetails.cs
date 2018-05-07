
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public virtual List<UserDetail> getUserDetails(string search, int Page, out int RowCount)
        {
            List<UserDetail> items = new List<UserDetail>();
              var user_name = search != null ?
                    new SqlParameter("@user_name", search) :
                    new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

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
            try
            {
                items = this.Database.SqlQuery<UserDetail>(
                                                "[sc_salesmanage_user].[getUserDetails] @user_name ,@page_number ,@page_size ,@row_count OUTPUT", user_name, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex) {
                RowCount = 0;
            }
            return items;
        }

        internal object getUserDetails(string serarch)
        {
            if (!String.IsNullOrEmpty(serarch))
            {
                List<ItemDetails> items = null;
                var item_code = serarch != null ?
                    new SqlParameter("@item_code", serarch) :
                    new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

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
                try
                {
                    items  = this.Database.SqlQuery<ItemDetails>(
                                                    "[sc_salesmanage_merchant].[getItemDetails]  @item_code ,@page_number ,@page_size ,@row_count OUTPUT", item_code, page_number, page_size, row_count)
                                                    .ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }

                return items[0];
            }
            return null;
        }
    }
}
