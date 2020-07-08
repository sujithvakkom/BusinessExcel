
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using BusinessExcel.Models;
using System.Diagnostics;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public virtual List<BonusWeek> getBonusWeeks(int year, DateTime dateFrom)
        {
            const string SELECT_BONUS_WEEEKS = @"SELECT ID, YEARNUMBER, WEEKNUMBER, DATEFROM, DATETO, DESCRIPTION
FROM  WEEKCALENDER where YEARNUMBER = @year
AND DATEFROM > @date_from
";

            var yearPar =
                  new SqlParameter("@year", year);
            var date_from =
                  new SqlParameter("@date_from", dateFrom);

            var bonusWeeks = this.Database.SqlQuery<BonusWeek>(SELECT_BONUS_WEEEKS, yearPar, date_from).ToList();
            return bonusWeeks;
        }
        public virtual List<BonusSettings> getBonusWeeksSettings(BonusWeek week,int? categeryID = null)
        {
            const string SELECT_BONUS_WEEEKS_SETTINGS = @"SELECT week_b.id,inv.item_code, 
       inv.description, 
       week_b.[incentive]
FROM WEEKCALENDER week_c
     INNER JOIN [WEEKBONUS] week_b ON week_c.ID = week_b.[WEEKCALENDERID]
     INNER JOIN sc_salesmanage_vansale.inventory_item_m AS inv ON inv.item_code = week_b.item_code
	 WHERE week_c.ID = @id
";
            var idPar =
                  new SqlParameter("@id", week.Id);
            var catPar = new SqlParameter("@cat", categeryID);

            var parArr = new List<SqlParameter>();
            parArr.Add(idPar);
            if (categeryID != null) parArr.Add(catPar);
            try
            {
                var bonusWeeksSettings = this.Database.SqlQuery<BonusSettings>(
                    SELECT_BONUS_WEEEKS_SETTINGS + (categeryID == null ? "" : " AND inv.category_id = @cat "),
                    parArr.ToArray()).ToList();
                return bonusWeeksSettings;
            }
            catch (Exception ex) {
                Debug.Assert(true, ex.Message);
            }
            return null;
        }

        public virtual void SaveBonusConfiguration(BonusItemConfigModel bonusConfig)
        {
            const string INSERT_BONUS_CONFIG = @"INSERT INTO [db_salesmanage_user].[WEEKBONUS]
           ([WEEKCALENDERID]
           ,[inventory_item_id]
           ,[item_code]
           ,[Incentive])
     VALUES
           (@week_id
           ,@inventory_item_id
           ,@item_code
           ,@incentive)
";

            var week_id =
                  new SqlParameter("@week_id", bonusConfig.WeekId);
            var inventory_item_id =
                  new SqlParameter("@inventory_item_id", bonusConfig.InventoryItemId);
            var item_code =
                  new SqlParameter("@item_code", bonusConfig.Item.item_code);
            var incentive =
                  new SqlParameter("@incentive", bonusConfig.IncentiveAmount);

            var bonusWeeks = this.Database.SqlQuery<BonusWeek>(INSERT_BONUS_CONFIG,
                week_id,
                inventory_item_id,
                item_code,
                incentive).ToList();
        }

        public virtual void DeleteBonusConfiguration(int id)
        {
            const string DELETE_BONUS_CONFIG = @"Delete [db_salesmanage_user].[WEEKBONUS]
            WHERE ID = @id
";

            var idPar =
                  new SqlParameter("@id", id);

            var bonusWeeks = this.Database.ExecuteSqlCommand(DELETE_BONUS_CONFIG, idPar);
        }



        public virtual List<BonusAchivement> getBonusAchievements(int userId)
        {
            const string SELECT_BONUS_ACHIV = @"SELECT inv.item_code, 
       inv.description, 
       SUM(quantity) quantity, 
       SUM(sale.value) sale, 
       SUM(total_bonus) bonus
FROM [sc_salesmanage_merchant].[merchant_daily_update] sale
     INNER JOIN [db_salesmanage_user].[WEEKBONUS] week_b ON sale.bonus_line_id = week_b.id
     INNER JOIN [db_salesmanage_user].[WEEKCALENDER] week_c ON week_c.id = week_b.WEEKCALENDERID
     INNER JOIN [sc_salesmanage_vansale].[inventory_item_m] inv ON week_b.inventory_item_id = inv.inventory_item_id
Where sale.user_id = @user_id
GROUP BY inv.item_code, 
         inv.description
";

            var user_id =
                  new SqlParameter("@user_id", userId);

            var bonusWeeks = this.Database.SqlQuery<BonusAchivement>(SELECT_BONUS_ACHIV, user_id).ToList();
            return bonusWeeks;
        }

    }
}