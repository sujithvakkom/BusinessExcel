
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using BusinessExcel.Models;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

    
        public DbSet<TargetAchievementView> TargetAchievementView { get; set; }
        //public DbSet<ItemDetails> ItemDetails { get; set; }

        public int UpdateTargetAchievementAmt(TargetAchievementView target)
        {
            int isUpdate = 0;
            try
            {


                const string UPDATE_TARGET = @" update db_salesmanage_user.user_target set 

entered_amt=@entered_amt
where target_id=@target_id and user_id=@user_id  and  category_id=@category_id ";

                var target_id = target.target_id != 0 ?
      new SqlParameter("@target_id", target.target_id) :
      new SqlParameter("@target_id", System.Data.SqlDbType.Int) { Value = 0 };

                var Entered_Amt = target.Entered_Amt != null ?
              new SqlParameter("@entered_amt", target.Entered_Amt) :
              new SqlParameter("@entered_amt", System.Data.SqlDbType.Decimal) { Value = DBNull.Value };


                var category_id = target.category_id != null ?
              new SqlParameter("@category_id", target.category_id) :
              new SqlParameter("@category_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };

                var user_id = target.user_id != null ?
                   new SqlParameter("@user_id", target.user_id) :
                   new SqlParameter("@user_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };





                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(target_id);
                parameterList.Add(Entered_Amt);
                parameterList.Add(category_id);
                parameterList.Add(user_id);





                isUpdate = this.Database.ExecuteSqlCommand(UPDATE_TARGET, parameterList.ToArray());
                return isUpdate;

            }
            catch
            {
                return isUpdate=0;
            }
            
        }

        public virtual List<TargetTotalView> getUsertargetTotalDetails(int TargetId)
        {
            List<TargetTotalView> items = new List<TargetTotalView>();   
            //var user_id = userId >0 ?
            //    new SqlParameter("@user_id", userId) :
            //    new SqlParameter("@user_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };
       
            var target_id = TargetId>0 ?
                new SqlParameter("@target_id", TargetId) :
                new SqlParameter("@target_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            //parameterList.Add(user_id);
            parameterList.Add(target_id);

            try
            {
          
                items = this.Database.SqlQuery<TargetTotalView>("[db_salesmanage_user].[User_Target_Achieved_Total_Values] @target_id", parameterList.ToArray()).ToList();


                //this.Database.SqlQuery<UserDetail>(
                //                                "[sc_salesmanage_user].[User_Target_Achieved_Total_Values] @user_id ,@target_id", user_id, target_id)
                //                                .ToList();
          
            }
            catch (Exception ex)
            {
                items = null;
            }
            return items;
        }
        public virtual List<TargetTotalView> getUsertargetTotalDetailsQTR(DateTime start_date)
        {
            List<TargetTotalView> items = new List<TargetTotalView>();
            //var user_id = userId >0 ?
            //    new SqlParameter("@user_id", userId) :
            //    new SqlParameter("@user_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            var qtr_start_date = (start_date) != default(DateTime) ?
            new SqlParameter("@start_date", start_date) :
            new SqlParameter("@start_date", System.Data.SqlDbType.DateTime) { Value = default(DateTime) };


         
            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            //parameterList.Add(user_id);
            parameterList.Add(qtr_start_date);

            try
            {

                items = this.Database.SqlQuery<TargetTotalView>("[db_salesmanage_user].[User_Target_Achieved_Total_Values_QTR] @start_date", parameterList.ToArray()).ToList();


                //this.Database.SqlQuery<UserDetail>(
                //                                "[sc_salesmanage_user].[User_Target_Achieved_Total_Values] @user_id ,@target_id", user_id, target_id)
                //                                .ToList();

            }
            catch (Exception ex)
            {
                items = null;
            }
            return items;
        }
        public virtual List<TargetAchievementView> getUsertargetAchievementDetailsView(int userId, int TargetId)
        {
            List<TargetAchievementView> items = new List<TargetAchievementView>();
            var user_id = userId > 0 ?
                new SqlParameter("@user_id", userId) :
                new SqlParameter("@user_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };

            var target_id = TargetId > 0 ?
                new SqlParameter("@target_id", TargetId) :
                new SqlParameter("@target_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(user_id);
            parameterList.Add(target_id);

            try
            {

                items = this.Database.SqlQuery<TargetAchievementView>("[db_salesmanage_user].[User_Target_Achieved_Details_ById] @user_id,@target_id", parameterList.ToArray()).ToList();


                //this.Database.SqlQuery<UserDetail>(
                //                                "[sc_salesmanage_user].[User_Target_Achieved_Total_Values] @user_id ,@target_id", user_id, target_id)
                //                                .ToList();

            }
            catch (Exception ex)
            {
                items = null;
            }
            return items;
        }

        public int UpdateEnteredBaseIncentive(TargetTotalView target)
        {
            int isUpdate = 0;
            try
            {

                                const string UPDATE_TARGET = @" update db_salesmanage_user.target_m set 

                entered_base_incentive=@entered_base_incentive
                where target_id=@target_id ";

                                var target_id = target.target_id != 0 ?
                      new SqlParameter("@target_id", target.target_id) :
                      new SqlParameter("@target_id", System.Data.SqlDbType.Int) { Value = 0 };

                                var entered_base_incentive = target.TotalEnteredBaseIncentive != null ?
                              new SqlParameter("@entered_base_incentive", target.TotalEnteredBaseIncentive) :
                              new SqlParameter("@entered_base_incentive", System.Data.SqlDbType.Decimal) { Value = DBNull.Value };

                

                                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                                parameterList.Add(target_id);
                                parameterList.Add(entered_base_incentive);
        
                                isUpdate = this.Database.ExecuteSqlCommand(UPDATE_TARGET, parameterList.ToArray());
                                return isUpdate;

                            }
                            catch
                            {
                                return isUpdate = 0;
                            }

        }

      
        public int UpdateEnteredIncentiveAmt(TargetTotalView target)
        {
                        int isUpdate = 0;
                        try
                        {

                            const string UPDATE_TARGET = @" update db_salesmanage_user.target_m set 

            entered_incentive_amt=@entered_incentive_amt
            where target_id=@target_id ";

                            var target_id = target.target_id != 0 ?
                  new SqlParameter("@target_id", target.target_id) :
                  new SqlParameter("@target_id", System.Data.SqlDbType.Int) { Value = 0 };

                            var entered_incentive_amt = target.TotalEnteredIncAmt != null ?
                          new SqlParameter("@entered_incentive_amt", target.TotalEnteredIncAmt) :
                          new SqlParameter("@entered_incentive_amt", System.Data.SqlDbType.Decimal) { Value = DBNull.Value };



                            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                            parameterList.Add(target_id);
                            parameterList.Add(entered_incentive_amt);

                            isUpdate = this.Database.ExecuteSqlCommand(UPDATE_TARGET, parameterList.ToArray());
                            return isUpdate;

                        }
                        catch(Exception ex)
                        {
                            return isUpdate = 0;
                        }

        }
        public int UpdateTargetStatus(TargetTotalView target)
        {
            int isUpdate = 0;
            try
            {

                const string UPDATE_TARGET = @" update db_salesmanage_user.target_m set 

            target_status=@target_status
            where target_id=@target_id ";

                var target_id = target.target_id != 0 ?
      new SqlParameter("@target_id", target.target_id) :
      new SqlParameter("@target_id", System.Data.SqlDbType.Int) { Value = 0 };

                var target_status = target.target_status >0 ?
              new SqlParameter("@target_status", target.target_status) :
              new SqlParameter("@target_status", System.Data.SqlDbType.Decimal) { Value = 0 };



                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(target_id);
                parameterList.Add(target_status);

                isUpdate = this.Database.ExecuteSqlCommand(UPDATE_TARGET, parameterList.ToArray());
                return isUpdate;

            }
            catch (Exception ex)
            {
                return isUpdate = 0;
            }

        }

        public int UpdateEnteredBaseIncentiveQTR(QuarterDetails qtr)
        {
            int isUpdate = 0;
            try
            {

               

                var user_id = qtr.user_id != 0 ?
      new SqlParameter("@user_id", qtr.user_id) :
      new SqlParameter("@user_id", System.Data.SqlDbType.Int) { Value = 0 };

                var qtr_start_date = qtr.start_date != default(DateTime) ?
new SqlParameter("@qtr_start_date", qtr.start_date) :
new SqlParameter("@qtr_start_date", System.Data.SqlDbType.Int) { Value = default(DateTime) };

                var entered_base_incentive = qtr.entered_base_incentive != null ?
              new SqlParameter("@entered_base_incentive", qtr.entered_base_incentive) :
              new SqlParameter("@entered_base_incentive", System.Data.SqlDbType.Decimal) { Value = DBNull.Value };

                var entered_incentive_amt = qtr.entered_incentive_amt != null ?
             new SqlParameter("@entered_base_incentive", qtr.entered_incentive_amt) :
             new SqlParameter("@entered_base_incentive", System.Data.SqlDbType.Decimal) { Value = DBNull.Value };


                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(user_id);
                parameterList.Add(qtr_start_date);
                parameterList.Add(entered_base_incentive);
                parameterList.Add(entered_incentive_amt);

                isUpdate = this.Database.ExecuteSqlCommand("[db_salesmanage_user].[update_quarter_details] @qtr_start_date,@user_id,@entered_base_incentive,@entered_incentive_amt", parameterList.ToArray());
                return isUpdate;

            }
            catch
            {
                return isUpdate = 0;
            }

        }

        public virtual List<TargetStatus> getTargetStatus()
        {
    
            List<TargetStatus> statuses = new List<TargetStatus>();
            const string SELECT_STATUS = @"select target_status_id, 
                                                target_status
                                                from [db_salesmanage_user].[target_status] ";
            try
            {
                statuses = this.Database.SqlQuery<TargetStatus>(SELECT_STATUS).ToList();
            }
            catch (Exception ex)
            {
                statuses = null;
            }
            return statuses;
        }

        public virtual TargetStatus getTargetStatusBtId(int status_id)
        {

            const string SELECT_STATUS = @"select target_status_id, 
                                                target_status
                                                from [db_salesmanage_user].[target_status]  where target_status_id=@target_status_id ";

            var target_status = status_id > 0 ?
              new SqlParameter("@target_status_id", status_id) :
              new SqlParameter("@target_status_id", System.Data.SqlDbType.Decimal) { Value = 0 };



            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(target_status);

     
            TargetStatus detail = null;
            try
            {
               var  statuses = this.Database.SqlQuery<TargetStatus>(SELECT_STATUS, parameterList.ToArray()).ToList();
                detail = statuses.Count > 0 ? statuses[0] : null;
            }
            catch (Exception ex)
            {
                detail= null;
            }
            return detail;
        }
       

    }
}
