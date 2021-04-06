
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
        public virtual BrandDetail getBrandDetail(string BrandId)
        {
            const string SELECT_BRAND = @"select brand_id,description from [brand_m] where brand_id = @brand_id";
            
            var brand_id = BrandId != null ?
                  new SqlParameter("@brand_id", BrandId) :
                  new SqlParameter("@brand_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            BrandDetail detail =
                this.Database.SqlQuery<BrandDetail>(SELECT_BRAND, brand_id).ToList()[0];
            return detail;
        }

        public virtual List<BrandDetail> getBrandDetails(string search, int Page, out int RowCount)
        {
            List<BrandDetail> items = new List<BrandDetail>();
              var filter = search != null ?
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
            try
            {
                items = this.Database.SqlQuery<BrandDetail>(
                                                "[getBrandDetails] @filter ,@page_number ,@page_size ,@row_count OUTPUT", filter, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex) {
                RowCount = 0;
            }
            return items;
        }
    }
}
