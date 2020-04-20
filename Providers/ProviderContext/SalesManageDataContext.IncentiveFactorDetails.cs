
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
        public virtual List<IncentiveFactor> getIncentiveFactorDetails(int? account, int? category, int Page, out int RowCount)
        {
            List<IncentiveFactor> items = new List<IncentiveFactor>();
            var Account = account != null ?
                    new SqlParameter("@account", account) :
                    new SqlParameter("@account", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var Catogery = category != null ?
                    new SqlParameter("@category", category) :
                    new SqlParameter("@category", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = 20 };

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
                items = this.Database.SqlQuery<IncentiveFactor>(
                                                "[sc_salesmanage_user].[getIncentiveFactor] @account, @category, @page_number, @page_size, @row_count OUTPUT", 
                                                Account,Catogery, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex) {
                RowCount = 0;
            }
            return items;
        }

        public virtual int insertUpdateIncentiveFactor(int? Account, int? Line, decimal? Factor, decimal? Threshold) {
            var account_id = new SqlParameter("@account_id", System.Data.SqlDbType.Int) { Value = Account };
            var category_id = new SqlParameter("@category_id", System.Data.SqlDbType.Int) { Value = Line };
            var factor = new SqlParameter("@factor", System.Data.SqlDbType.Decimal) { Value = Factor };
            var @threshold = new SqlParameter("@threshold", System.Data.SqlDbType.Decimal) { Value = Threshold };
            this.Database.ExecuteSqlCommand(@"[db_salesmanage_user].[insertIncentiveFactor] @account_id, @category_id, @factor, @threshold",
                        account_id,
                        category_id,
                        factor,
                        threshold);
            return 0;
        }

    }
}
