
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
    }
}
