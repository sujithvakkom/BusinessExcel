
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
        public virtual CategoryDetail getCategoryDetails(string serarch)
        {
            CategoryDetail result = null;
            if (!String.IsNullOrEmpty(serarch))
            {
                var filter = serarch != null ?
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

                var items = this.Database.SqlQuery<CategoryDetail>(
                                                "[sc_salesmanage_vansale].[getCategoryDetails]  @filter ,@page_number ,@page_size ,@row_count OUTPUT", filter, page_number, page_size, row_count)
                                                .ToList();
                if (items.Count > 0)
                    result = items[0];
            }
            return result;
        }

        public virtual List<CategoryDetail> getCategoryDetails(string search, int Page, out int RowCount)
        {
            List<CategoryDetail> category = new List<CategoryDetail>();

            var filter = string.IsNullOrEmpty(search) ?
                new SqlParameter("@filter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@filter", search);
            //filter.Value = DBNull.Value;

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value=DBNull.Value};
            //page_size.Value = DBNull.Value;

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value=DBNull.Value};
            //page_size.Value = DBNull.Value;

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) { Direction = System.Data.ParameterDirection.Output } :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value, Direction = System.Data.ParameterDirection.Output };
            //db.getItemDetailsImport(null, null, null);
            try
            {
                category = this.Database.SqlQuery<CategoryDetail>(
                                                "[sc_salesmanage_vansale].[getCategoryDetails]  @filter ,@page_number ,@page_size ,@row_count OUTPUT", filter, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return category;
        }


        public virtual CategoryDetail getCategoryDetails(int CategoryID)
        {
            CategoryDetail result = null;
            
            var category_id =
                new SqlParameter("@category_id", CategoryID);

            var items = this.Database.SqlQuery<CategoryDetail>(
                                                "SELECT category_id ,description FROM [sc_salesmanage_merchant].[category] WHERE category_id = @category_id",
                                                category_id)
                                                .ToList();
                if (items.Count > 0)
                    result = items[0];
            return result;

        }

    }
}
