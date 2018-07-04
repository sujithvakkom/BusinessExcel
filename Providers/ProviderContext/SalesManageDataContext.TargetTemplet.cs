
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        /*
DECLARE @RC int
DECLARE @start_date date
DECLARE @end_date date
DECLARE @location_in nvarchar(1)
DECLARE @user_in nvarchar(1)
DECLARE @description_in nvarchar(1)
DECLARE @base_incentive_in nvarchar(1)
DECLARE @target_line_in [db_salesmanage_user].[target_category_line]

EXECUTE @RC = [db_salesmanage_user].[create_update_target] 
   @start_date
  ,@end_date
  ,@location_in
  ,@user_in
  ,@description_in
  ,@base_incentive_in
  ,@target_line_in
GO

         private readonly string cmd = "[db_salesmanage_user].[create_update_target] @start_date,@end_date,@location_in,@user_in,@description_in,@base_incentive_in,@target_line_in";

         */
         private readonly string cmd = "[db_salesmanage_user].[create_update_target] @start_date, @end_date, @location_in, @user_in, @description_in, @base_incentive_in, @target_line_in ,@message OUTPUT ,@curr_target_id OUTPUT";
        public virtual int createUpdateTarget(BaseTarget target, out string Message)
        {
            int result = -1;

            var start_date = new SqlParameter("@start_date", target.StartDate);

            var end_date = new SqlParameter("@end_date", target.EndDate);

            var location_in = String.IsNullOrEmpty(target.Location) ?
                new SqlParameter("@location_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@location_in", target.Location);

            var user_in = String.IsNullOrEmpty(target.UserName) ?
                new SqlParameter("@user_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@user_in", target.UserName);

            var description_in = String.IsNullOrEmpty(target.Description) ?
                new SqlParameter("@description_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value }:
                new SqlParameter("@description_in", target.Description) ;

            var base_incentive_in =
                target.BaseIncentive == null ?
                new SqlParameter("@base_incentive_in", SqlDbType.Decimal) { Value = DBNull.Value } :
                new SqlParameter("@base_incentive_in", target.BaseIncentive);

            var data = target.getTargetLine();
            data.TableName = "target_line_in";
            var target_line_in = new SqlParameter("@target_line_in", System.Data.SqlDbType.Structured) { Value = data, TypeName = "target_category_line" };

            var message =
                new SqlParameter("@message", SqlDbType.NVarChar,-1) { Value = DBNull.Value ,Direction=ParameterDirection.Output};

            var curr_target_id = new SqlParameter("@curr_target_id", SqlDbType.Int) { Direction = ParameterDirection.Output };

            try
            {
                result = this.Database.ExecuteSqlCommand(cmd, 
                    start_date, 
                    end_date, 
                    location_in, 
                    user_in, 
                    description_in, 
                    base_incentive_in, 
                    target_line_in,
                    message, 
                    curr_target_id);
                Message = message.Value.ToString();
                result = int.Parse(curr_target_id.Value.ToString());
            }
            catch (Exception ex) {
                result = -1;
                Message = ex.Message;
            }
            return result;
        }

        public virtual List<TargetTemplet> getTargetTempletDetails(string search, int Page, out int RowCount)
        {

            List<TargetTemplet> items = new List<TargetTemplet>();
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
                items = this.Database.SqlQuery<TargetTemplet>(
                                                "[sc_salesmanage_vansale].[getTargetTempletDetails]  @filter ,@page_number ,@page_size ,@row_count OUTPUT", filter, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return items;
        }


        public virtual List<LineTarget> getTargetTempletLineDetails(int? search)
        {
            int result = -1;
            List<LineTarget> items = new List<LineTarget>();
            var target_id = search != null ?
                  new SqlParameter("@target_id", search) :
                  new SqlParameter("@target_id", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            try
            {
                items = this.Database.SqlQuery<LineTarget>(
                                                " [sc_salesmanage_vansale].[getTargetTempletLineDetails]  @target_id", target_id)
                                                .ToList();
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return items;
        }

    }
}
