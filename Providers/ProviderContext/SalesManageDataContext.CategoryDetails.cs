
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public virtual CategoryDetail AddCategoryDetails(string Description)
        {
            CategoryDetail result = null;
            var category_id =
                new SqlParameter("@category_id", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            var description = string.IsNullOrEmpty(Description) ?
                new SqlParameter("@description", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@description", Description);

            int? row = null;
            var category_id_out = row != null ?
                new SqlParameter("@category_id_out", row) :
                new SqlParameter("@category_id_out", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            category_id_out.Direction = System.Data.ParameterDirection.Output;

            var items = this.Database.SqlQuery<CategoryDetail>(
                                            @"EXECUTE [sc_salesmanage_vansale].[update_category] 
   @category_id
  , @description
  , @category_id_out OUTPUT", category_id, description, category_id_out)
                                            .ToList();
            int catogeryId;
            int.TryParse(category_id_out.Value.ToString(), out catogeryId);
            result = getCategoryDetails(catogeryId);
            return result;
        }

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

        public virtual List<CategoryDetail> getCategorySettingsDetails(int? category, int Page, out int RowCount)
        {
            List<CategoryDetail> categoryList = new List<CategoryDetail>();

            var filter = category==null ?
                new SqlParameter("@category", System.Data.SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@category", category);
            //filter.Value = DBNull.Value;

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            //page_size.Value = DBNull.Value;

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            //page_size.Value = DBNull.Value;

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) { Direction = System.Data.ParameterDirection.Output } :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value, Direction = System.Data.ParameterDirection.Output };
            //db.getItemDetailsImport(null, null, null);
            try
            {
                categoryList = this.Database.SqlQuery<CategoryDetail>(
                                                "[sc_salesmanage_user].[getCatogerySettings] @category ,@page_number ,@page_size ,@row_count OUTPUT", filter, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return categoryList;
        }

        public virtual int insertUpdateCategorysettings(int? Line,string Description, decimal? BaseIncentive, decimal? TotalSaleFactor, decimal? CategorySellinFactor)
        {
            BaseIncentive = BaseIncentive == null ? 0 : BaseIncentive / 100;
            TotalSaleFactor = TotalSaleFactor == null ? 0 : TotalSaleFactor / 100;
            CategorySellinFactor = CategorySellinFactor == null ? 0 : CategorySellinFactor / 100;
            var category_id = new SqlParameter("@category_id", System.Data.SqlDbType.Int) { Value = Line };
            var description = new SqlParameter("@description", System.Data.SqlDbType.NVarChar) { Value = (object)(String.IsNullOrEmpty( Description )?(object)DBNull.Value:(object)Description)};
            var base_incentive = new SqlParameter("@base_incentive", System.Data.SqlDbType.Decimal) { Value = BaseIncentive };
            var total_sale_factor = new SqlParameter("@total_sale_factor", System.Data.SqlDbType.Decimal) { Value = TotalSaleFactor };
            var category_sellin_factor = new SqlParameter("@category_sellin_factor", System.Data.SqlDbType.Decimal) { Value = CategorySellinFactor };
            try
            {
                this.Database.ExecuteSqlCommand(@"[db_salesmanage_user].[insertCategorySettings] @category_id ,@description ,@base_incentive ,@total_sale_factor ,@category_sellin_factor",
                            category_id,
                            description,
                            base_incentive,
                            total_sale_factor, 
                            category_sellin_factor);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }
    }
}
