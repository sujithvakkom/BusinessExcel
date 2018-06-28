
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public DbSet<Roster> Roster { get; set; }
        // public DbSet<Roster> RosterList { get; set; }

        //public DbSet<ItemDetails> ItemDetails { get; set; }


        public int InsertUpdateRoster(Roster roster, bool isInsert)
        {
            const string INSERT_ROSTER = @"insert into db_salesmanage_user.roster (user_id,user_name,u_name,location_id,location_name,start_date,end_date,target_id,target_amt) 
                                                 values((SELECT [db_salesmanage_user].[get_user_id] (@user_name)),@user_name,@u_name,@location_id,@location_name,@start_date,@end_date,@target_id,@target_amt)";

            const string UPDATE_ROSTER = @" update db_salesmanage_user.roster set user_id=(SELECT [db_salesmanage_user].[get_user_id] (@user_name)),

user_name=@user_name,u_name=@u_name,location_id=@location_id,location_name=@location_name,start_date=@start_date,end_date=@end_date,target_id=@target_id,target_amt=@target_amt

where roster_id=@roster_id";

            var roster_id = roster.roster_id != 0 ?
  new SqlParameter("@roster_id", roster.roster_id) :
  new SqlParameter("@roster_id", System.Data.SqlDbType.NVarChar) { Value = 0 };


            var name = roster.name != null ?
                  new SqlParameter("@user_name", roster.name) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

          //  var u_name = roster.u_name != null ?
          //new SqlParameter("@u_name", roster.u_name) :
          //new SqlParameter("@u_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            var location_id = roster.location_id != null ?
          new SqlParameter("@location_id", roster.location_id) :
          new SqlParameter("@location_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

          //  var location_name = roster.location_name != null ?
          //new SqlParameter("@location_name", roster.location_name) :
          //new SqlParameter("@location_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var start_date = roster.start_date != null ?
          new SqlParameter("@start_date", roster.start_date) :
          new SqlParameter("@start_date", System.Data.SqlDbType.DateTime) { Value = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue };

            var end_date = roster.end_date != null ?
          new SqlParameter("@end_date", roster.end_date) :
          new SqlParameter("@end_date", System.Data.SqlDbType.DateTime) { Value = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue };

            //   int? page = null;
            //  var target_id = roster.target_id != null ?
            //var target_id = new SqlParameter("@target_id", roster.target_id);
            ////  new SqlParameter("@target_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

   //         var target_amt = roster.target_amt != null ?
   //new SqlParameter("@target_amt", roster.target_amt) :
   //new SqlParameter("@target_amt", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(name);
           // parameterList.Add(u_name);
            parameterList.Add(location_id);
          //  parameterList.Add(location_name);
            parameterList.Add(start_date);
            parameterList.Add(end_date);
         //   parameterList.Add(target_id);
          //  parameterList.Add(target_amt);

            var isInsertUpdate = 0;

            if (isInsert)
            {
                isInsertUpdate = this.Database.ExecuteSqlCommand(INSERT_ROSTER, parameterList.ToArray());
            }
            else
            {
                parameterList.Add(roster_id);
                isInsertUpdate = this.Database.ExecuteSqlCommand(UPDATE_ROSTER, parameterList.ToArray());
            }

            return isInsertUpdate;
        }
        public virtual TargetMasterDetails getTargetDetail(string target_id)
        {
            const string SELECT_TARGET = @"select target_id, 
                                                description
                                                from [db_salesmanage_user].[target_m] 
                                            where 
                                                target_id = @target_id";

            var user_name = target_id != null ?
                  new SqlParameter("@target_id", target_id) :
                  new SqlParameter("@target_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            TargetMasterDetails detail =
                this.Database.SqlQuery<TargetMasterDetails>(SELECT_TARGET, user_name).ToList()[0];
            return detail;
        }


        public virtual List<TargetMasterDetails> getTargetDetails(string search, int Page, out int RowCount)
        {
            List<TargetMasterDetails> items = new List<TargetMasterDetails>();
            var description = search != null ?
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
                items = this.Database.SqlQuery<TargetMasterDetails>(
                                                "[sc_salesmanage_user].[getTargetDetails] @filter ,@page_number ,@page_size ,@row_count OUTPUT", description, page_number, page_size, row_count)
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
