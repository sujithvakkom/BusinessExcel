
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        internal int insertUpdateGlobalSettings(GlobalSettings globalSettings)
        {

            var line_achieve_acc = new SqlParameter("@line_achieve_acc", System.Data.SqlDbType.Decimal) { Value = globalSettings.line_achieve_acc };
            var bonus_achieve_acc = new SqlParameter("@bonus_achieve_acc", System.Data.SqlDbType.Decimal) { Value = globalSettings.bonus_achieve_acc };
            var min_total_achievement = new SqlParameter("@min_total_achievement", System.Data.SqlDbType.Decimal) { Value = globalSettings.min_total_achievement };
            var min_line_achievement = new SqlParameter("@min_line_achievement", System.Data.SqlDbType.Decimal) { Value = globalSettings.min_line_achievement };
            var min_bonus_achievement = new SqlParameter("@min_bonus_achievement", System.Data.SqlDbType.Decimal) { Value = globalSettings.min_bonus_achievement };
            var cap_base_incentive = new SqlParameter("@cap_base_incentive", System.Data.SqlDbType.Decimal) { Value = globalSettings.cap_base_incentive };
            var enabled = new SqlParameter("@enabled", System.Data.SqlDbType.Bit) { Value = globalSettings.enabled };
            var base_incentive_pct = new SqlParameter("@base_incentive_pct", System.Data.SqlDbType.Decimal) { Value = globalSettings.base_incentive_pct };
            var global_base_incentive_cap = new SqlParameter("@global_base_incentive_cap", System.Data.SqlDbType.Decimal) { Value = globalSettings.global_base_incentive_cap };
            var global_acc_factor = new SqlParameter("@global_acc_factor", System.Data.SqlDbType.Decimal) { Value = globalSettings.global_acc_factor };
            try
            {
                this.Database.ExecuteSqlCommand(@"[insertUpdateGlobalSettings] 
	                                                @line_achieve_acc
                                                  ,@bonus_achieve_acc
                                                  ,@min_total_achievement
                                                  ,@min_line_achievement
                                                  ,@min_bonus_achievement
                                                  ,@cap_base_incentive
                                                  ,@enabled
                                                  ,@base_incentive_pct
                                                  ,@global_base_incentive_cap
                                                  ,@global_acc_factor",
                                                line_achieve_acc
                                              , bonus_achieve_acc
                                              , min_total_achievement
                                              , min_line_achievement
                                              , min_bonus_achievement
                                              , cap_base_incentive
                                              , enabled
                                              , base_incentive_pct
                                              , global_base_incentive_cap
                                              , global_acc_factor);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;

        }



        public GlobalSettings getGlobalSettings()
        {
            GlobalSettings result = null;
            var items = this.Database.SqlQuery<GlobalSettings>(
                                               @"SELECT TOP (1) line_achieve_acc ,
                                               bonus_achieve_acc,
                                               min_total_achievement,
                                               min_line_achievement,
                                               min_bonus_achievement,
                                               cap_base_incentive,
                                               enabled,
                                               date,
                                               base_incentive_pct,
                                               global_base_incentive_cap,
                                               global_acc_factor
                                               FROM [target_settings]")
                                                .ToList();
            if (items.Count > 0)
                result = items[0];
            return result;

        }
    }
}