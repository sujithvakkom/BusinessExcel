
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
DECLARE @target_line_in [target_category_line]

EXECUTE @RC = [create_update_target] 
   @start_date
  ,@end_date
  ,@location_in
  ,@user_in
  ,@description_in
  ,@base_incentive_in
  ,@target_line_in
GO

         private readonly string cmd = "[create_update_target] @start_date,@end_date,@location_in,@user_in,@description_in,@base_incentive_in,@target_line_in";

         */
        private readonly string cmd = "[create_update_target] @start_date, @end_date, @location_in, @user_in, @description_in, @base_incentive_in, @target_teplat_id, @target_line_in ,@message OUTPUT ,@curr_target_id OUTPUT";
        public virtual int createUpdateTarget(BaseTarget target, out string Message)
        {
            int result = -1;

            var start_date =
                target.StartDate == DateTime.MinValue ?
                new SqlParameter("@start_date", SqlDbType.DateTime) { Value = DBNull.Value } :
                new SqlParameter("@start_date", target.StartDate);


            var end_date =
                target.EndDate == DateTime.MinValue ?
                new SqlParameter("@end_date", SqlDbType.DateTime) { Value = DBNull.Value } :
                    new SqlParameter("@end_date", target.EndDate);

            var location_in = String.IsNullOrEmpty(target.Location) ?
                new SqlParameter("@location_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@location_in", target.Location);

            var user_in = String.IsNullOrEmpty(target.UserName) ?
                new SqlParameter("@user_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@user_in", target.UserName);

            var description_in = String.IsNullOrEmpty(target.Description) ?
                new SqlParameter("@description_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@description_in", target.Description);

            var base_incentive_in =
                target.BaseIncentive == null ?
                new SqlParameter("@base_incentive_in", SqlDbType.Decimal) { Value = DBNull.Value } :
                new SqlParameter("@base_incentive_in", target.BaseIncentive);

            var target_teplat_id =
                target.TargetTemplate == null ?
                new SqlParameter("@target_teplat_id", SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@target_teplat_id", target.TargetTemplate);

            var data = target.getTargetLine();
            data.TableName = "target_line_in";
            var target_line_in = new SqlParameter("@target_line_in", System.Data.SqlDbType.Structured) { Value = data, TypeName = "[target_category_line]" };

            var message =
                new SqlParameter("@message", SqlDbType.NVarChar, -1) { Value = DBNull.Value, Direction = ParameterDirection.Output };

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
                    target_teplat_id,
                    target_line_in,
                    message,
                    curr_target_id);
                Message = message.Value.ToString();
                result = int.Parse(curr_target_id.Value.ToString());
            }
            catch (Exception ex)
            {
                result = -1;
                Message = ex.Message;
            }
            return result;
        }
        /*
DECLARE @RC int
DECLARE @start_date date
DECLARE @end_date date
DECLARE @location_in nvarchar(1)
DECLARE @user_in nvarchar(1)
DECLARE @description_in nvarchar(1)
DECLARE @base_incentive_in nvarchar(1)
DECLARE @target_line_in [target_category_line]

EXECUTE @RC = [create_update_target] 
   @start_date
  ,@end_date
  ,@location_in
  ,@user_in
  ,@description_in
  ,@base_incentive_in
  ,@target_line_in
GO

         private readonly string cmd = "[create_update_target] @start_date,@end_date,@location_in,@user_in,@description_in,@base_incentive_in,@target_line_in";

         */
        private readonly string cmd1 = "[create_update_se_target] @start_date, @end_date, @location_in, @user_in, @description_in, @base_incentive_in, @base_incentive_qtr_in, @target_teplat_id, @target_line_in ,@message OUTPUT ,@curr_target_id OUTPUT";
        public virtual int createUpdateTargetSE(BaseTarget target, out string Message)
        {
            int result = -1;

            var start_date =
                target.StartDate == DateTime.MinValue ?
                new SqlParameter("@start_date", SqlDbType.DateTime) { Value = DBNull.Value } :
                new SqlParameter("@start_date", target.StartDate);


            var end_date =
                target.EndDate == DateTime.MinValue ?
                new SqlParameter("@end_date", SqlDbType.DateTime) { Value = DBNull.Value } :
                    new SqlParameter("@end_date", target.EndDate);

            var location_in = String.IsNullOrEmpty(target.Location) ?
                new SqlParameter("@location_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@location_in", target.Location);

            var user_in = String.IsNullOrEmpty(target.UserName) ?
                new SqlParameter("@user_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@user_in", target.UserName);

            var description_in = String.IsNullOrEmpty(target.Description) ?
                new SqlParameter("@description_in", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@description_in", target.Description);

            var base_incentive_in =
                target.BaseIncentive == null ?
                new SqlParameter("@base_incentive_in", SqlDbType.Decimal) { Value = DBNull.Value } :
                new SqlParameter("@base_incentive_in", target.BaseIncentive);

            var base_incentive_qtr_in =
                target.BaseIncentive == null ?
                new SqlParameter("@base_incentive_qtr_in", SqlDbType.Decimal) { Value = DBNull.Value } :
                new SqlParameter("@base_incentive_qtr_in", target.base_incentive_qtr);

            var target_teplat_id =
                target.TargetTemplate == null ?
                new SqlParameter("@target_teplat_id", SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@target_teplat_id", target.TargetTemplate);

            var data = target.getTargetLine();
            data.TableName = "target_line_in";
            var target_line_in = new SqlParameter("@target_line_in", System.Data.SqlDbType.Structured) { Value = data, TypeName = "target_category_line" };

            var message =
                new SqlParameter("@message", SqlDbType.NVarChar, -1) { Value = DBNull.Value, Direction = ParameterDirection.Output };

            var curr_target_id = new SqlParameter("@curr_target_id", SqlDbType.Int) { Direction = ParameterDirection.Output };

            try
            {
                result = this.Database.ExecuteSqlCommand(cmd1,
                    start_date,
                    end_date,
                    location_in,
                    user_in,
                    description_in,
                    base_incentive_in,
                    base_incentive_qtr_in,
                    target_teplat_id,
                    target_line_in,
                    message,
                    curr_target_id);
                Message = message.Value.ToString();
                result = int.Parse(curr_target_id.Value.ToString());
            }
            catch (Exception ex)
            {
                result = -1;
                Message = ex.Message;
            }
            return result;
        }

        public virtual List<TargetTemplet> getTargetTempletDetails(string search, int? PageNum, int? PageSize, out int RowCount, int? LocationID = null)
        {

            List<TargetTemplet> items = new List<TargetTemplet>();

            var filter = search != null ?
                  new SqlParameter("@filter", search) :
                  new SqlParameter("@filter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var location_id = LocationID != null ?
                  new SqlParameter("@location_id", LocationID) :
                  new SqlParameter("@location_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var page_size = PageSize != null ?
                new SqlParameter("@page_size", PageSize) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            var page_number = PageNum != null ?
                new SqlParameter("@page_number", PageNum) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;

            try
            {
                items = this.Database.SqlQuery<TargetTemplet>(
                                                "[getTargetTempletDetails]  @filter ,@location_id ,@page_number ,@page_size ,@row_count OUTPUT",
                                                filter,
                                                location_id,
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

        public virtual List<LineTarget> getTargetTempletLineDetails(int? search, bool isBalance = true, int? userID=null)
        {
            int result = -1;
            List<LineTarget> items = new List<LineTarget>();
            var target_id = search != null ?
                  new SqlParameter("@target_id", search) :
                  new SqlParameter("@target_id", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            var balance =
                  new SqlParameter("@balance", Convert.ToInt32(isBalance));

            var user_id = userID != null ?
                  new SqlParameter("@user_id", userID) :
                  new SqlParameter("@user_id", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            try
            {
                items = this.Database.SqlQuery<LineTarget>(
                                                " [getTargetTempletLineDetails]  @target_id, @balance, @user_id", 
                                                target_id,
                                                balance,
                                                user_id)
                                                .ToList();
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return items;
        }

        public virtual BaseTarget getTargetTemplet(int? TargetTempletID) {
            BaseTarget result = null;
            
            var target_id = TargetTempletID != null ?
                new SqlParameter("@target_id", TargetTempletID) :
                new SqlParameter("@target_id", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };


            try
            {
                result = (BaseTarget)(this.Database.SqlQuery<_BaseTarget>(
                                                @"SELECT t.[target_id] AS    TargetTemplate ,
       NULL AS             UserName ,
       t.[description] AS  Description ,
       r.location_id AS    Location ,
       r.start_date AS     StartDate ,
       r.end_date AS       EndDate ,
       NULL AS             TotalTarget ,
       t.base_incentive AS BaseIncentive
FROM [target_m] AS t INNER JOIN [roster] AS r ON t.roster_id = r.roster_id
WHERE t.target_id = @target_id",
                                                target_id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }

        public virtual List<CategoryTarget> getCategoryTarget(string UserName, DateTime StartDate, DateTime EndDate)
        {
            var start_date = StartDate != null ?
                new SqlParameter("@start_date", StartDate) :
                new SqlParameter("@start_date", System.Data.SqlDbType.DateTime) { Value = DBNull.Value };
            var end_date = EndDate != null ?
                new SqlParameter("@end_date", EndDate) :
                new SqlParameter("@end_date", System.Data.SqlDbType.DateTime) { Value = DBNull.Value };
            var user_name = !string.IsNullOrEmpty(UserName) ?
                new SqlParameter("@user_name", UserName) :
                new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            string cmd = @"SELECT u.user_name ,
       u.display_name ,
       t.description AS target_description ,
       '' AS  date ,
       c.description AS category_description ,
       ut.value ,
       ut.has_bonus,
c.category_id
FROM roster AS r INNER JOIN [target_m] AS t ON r.roster_id = t.roster_id
                                     INNER JOIN [user_target] AS ut ON t.target_id = ut.target_id
                                     INNER JOIN user_m AS u ON t.user_id = u.user_id
                                     INNER JOIN [category] AS c ON ut.category_id = c.category_id
WHERE r.start_date >= @start_date
      AND
      r.end_date <= @end_date
      AND
      u.user_name = @user_name";
            try
            {
                var result = this.Database.SqlQuery<CategoryTarget>(cmd,
                    start_date,
                    end_date,
                    user_name);
                return result.ToList();
            }
            catch (Exception ex) {
                return null;
            }
        }
    }
}
