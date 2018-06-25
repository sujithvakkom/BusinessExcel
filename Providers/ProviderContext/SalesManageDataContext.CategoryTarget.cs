
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
        //public virtual BrandDetail getBrandDetail(string BrandId)
        //{
        //    const string SELECT_BRAND = @"select brand_id,description from [sc_salesmanage_vansale].[brand_m] where brand_id = @brand_id";

        //    var brand_id = BrandId != null ?
        //          new SqlParameter("@brand_id", BrandId) :
        //          new SqlParameter("@brand_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

        //    BrandDetail detail =
        //        this.Database.SqlQuery<BrandDetail>(SELECT_BRAND, brand_id).ToList()[0];
        //    return detail;
        //}
        /*
         * int pageNumber, int pageSize, string sort, String sortdir, out int count, 
            ActionViewFilters Filters)*/

        public virtual List<CategoryTarget> getCategoryTarget(int pageNumber, int? pageSize, string sort, String sortdir, out int count)
        {
            List<CategoryTarget> items = new List<CategoryTarget>();

            int? page = null;
            var page_size = pageSize != null ?
                new SqlParameter("@page_size", pageSize) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? page_num = pageNumber;
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
                items = this.Database.SqlQuery<CategoryTarget>(
                                                "[sc_salesmanage_merchant].[getCategoryTarget] @page_number, @page_size, @row_count OUTPUT", page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out count);
            }
            catch (Exception ex)
            {
                count = 0;
            }
            return items;
        }
    }
}