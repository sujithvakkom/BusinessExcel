
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Models;
using BusinessExcel.Extentions;
using System.Data.SqlClient;
using System.Collections.Generic;



namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public List<TargetImportExportDetails> GetTargetExport(BackupTargetFilter Filters)
        {
            var result = new List<TargetImportExportDetails>();
            #region COMMAND
            var sqlCmd = @"SELECT t.description TargetDescription, 
       u.emp_code_lookup EmployCode, 
       u.display_name EmployName, 
       c.category_id CategoryID, 
       c.description CategoryDescription, 
       l.location_id LocationID, 
       l.description LocationDescription, 
       tu.value TargetValue, 
       tu.line_achieve_acc LineAchivementAcc, 
       tu.bonus_achieve_acc BonusAchievementAcc, 
       tu.min_total_achievement MinTotalAchievement, 
       tu.min_line_achievement MinLineAchievement, 
       tu.min_bonus_achievement MinBonusAchievement, 
       tu.cap_base_incentive IncentiveCap
FROM [db_salesmanage_user].[target_m] t
     INNER JOIN [db_salesmanage_user].[user_target] tu ON t.target_id = tu.target_id
     INNER JOIN [sc_salesmanage_user].[user_m] u ON tu.user_id = u.user_id
     INNER JOIN [sc_salesmanage_merchant].[category] c ON tu.category_id = c.category_id
     INNER JOIN [db_salesmanage_user].[roster] r ON tu.roster_id = r.roster_id
     INNER JOIN [sc_salesmanage_user].[location_m] l ON r.location_id = l.location_id
	 WHERE t.description = @description";
            #endregion
            var description = !string.IsNullOrEmpty(Filters.Month)?
                new SqlParameter("@description", Filters.Month) :
                new SqlParameter("@description", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(description);
            result = this.Database.SqlQuery<TargetImportExportDetails>(
                sqlCmd,
                parameterList.ToArray()).ToList();
            return result;
        }
    }
}