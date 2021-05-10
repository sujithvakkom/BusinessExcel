using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{

    public partial class SalesManageDataContext : DbContext
    {
        #region CMD
        const string cmdGetTarget = @"SELECT u.user_name AS UserName,
       ISNULL(display_name, u.first_name+' '+u.second_name) AS FullName,
       l.description AS Location,
       t.description AS Period,
       c.description AS Catogery,
       ISNULL(ut.value, 0) AS Target,
       ISNULL(d_u.value, 0) AS Achievement
FROM
(
    SELECT sal.user_id,
           sal.target_id,
           inv.category_id,
           t.roster_id,
           SUM(ISNULL(sal.value, 0)) AS value
    FROM [dbo].[merchant_daily_update] AS sal
         INNER JOIN [dbo].[inventory_item_m] AS inv ON sal.model_id = inv.model_id
         INNER JOIN [dbo].[target_m] AS t ON sal.target_id = t.target_id
    WHERE sal.target_id IS NOT NULL
          AND sal.user_id = @user_id
    GROUP BY sal.user_id,
             sal.target_id,
             t.roster_id,
             inv.category_id
) d_u
FULL JOIN [dbo].[user_target] AS ut ON d_u.target_id = ut.target_id
                                                       AND d_u.user_id = ut.user_id
                                                       AND ut.roster_id = d_u.roster_id
                                                       AND d_u.category_id = ut.category_id
INNER JOIN dbo.category AS c ON ISNULL(d_u.category_id, ut.category_id) = c.category_id
INNER JOIN dbo.user_m AS u ON ISNULL(ut.user_id, d_u.user_id) = u.user_id
                                              AND u.user_id = @user_id
INNER JOIN [dbo].[target_m] AS t ON ISNULL(d_u.target_id, ut.target_id) = t.target_id
INNER JOIN [dbo].[roster] AS r ON r.roster_id = t.roster_id
INNER JOIN [dbo].[location_m] AS l ON r.location_id = l.location_id
                                                      AND l.deleted = 0
ORDER BY 
r.start_date DESC";
        #endregion
        public List<UserTargetDetailsView> getUserTargetDetails(string UserName)
        {
            int? UserID = null;
            if (UserName != null)
                UserID = getUserID(UserName);
            else return null;
            var user_id = UserID != null ?
                  new SqlParameter("@user_id", UserID) :
                  new SqlParameter("@user_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var result =
                this.Database.SqlQuery<UserTargetDetailsView>(cmdGetTarget, user_id).ToList();
            var summary = result.GroupBy(g => new
            {
                g.Period,
                g.Location,
                g.FullName
            }).Select(s => new UserTargetDetailsView()
            {
                Period = s.First().Period,
                Location = s.First().Location,
                FullName = s.First().FullName,
                Catogery = "Total",
                Target = s.Sum(x => x.Target),
                Achievement = s.Sum(x => x.Achievement)
            }).ToList();
            foreach (var x in summary)
            {
                x.Lines = (from lin in result where lin.Period == x.Period select lin).ToList();
            }
            return summary.ToList();
        }
        private int? getViewer_Id()
        {
            return null;
        }

        public virtual int getUserID(string userName)
        {
            const string SELECT_USER = @"select user_id
                                                from [dbo].[user_m] 
                                            where 
                                                user_name = @user_name";

            var user_name = userName != null ?
                  new SqlParameter("@user_name", userName) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int detail =
                this.Database.SqlQuery<int>(SELECT_USER, user_name).ToList()[0];
            return detail;
        }

    }
}