
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        /*
SELECT u.user_name AS                                           UserName ,
       ISNULL(display_name , u.first_name+' '+u.second_name) AS FullName ,
       l.description AS                                         Location ,
       t.description AS                                         Period ,
       c.description AS                                         Catogery ,
       ut.value AS                                              Target ,
       ut.value * 100 / ut1.value AS                            AllocatedPercentage
FROM target_m AS t INNER JOIN user_m AS u ON t.user_id = u.user_id
                   INNER JOIN user_target AS ut ON t.target_id = ut.target_id
                                                   AND
                                                   t.user_id = ut.user_id
                                                   AND
                                                   t.roster_id = ut.roster_id
                   INNER JOIN category AS c ON ut.category_id = c.category_id
                                                                       AND
                                                                       ut.category_id = c.category_id
                   INNER JOIN target_m AS t1 ON t1.user_id IS NULL
                                                AND
                                                t1.roster_id = t.roster_id
                   INNER JOIN [roster] AS r ON r.roster_id = t.roster_id
                   INNER JOIN [location_m] AS l ON r.location_id = l.location_id
                                                                         AND
                                                                         l.deleted = 0
                   INNER JOIN user_target AS ut1 ON t1.target_id = ut1.target_id
                                                    AND
                                                    ut1.category_id = ut.category_id
WHERE t1.target_id = target_templet_id
         */
        private readonly string cmdGetLocationTargetAssignments = @"
SELECT u.user_name AS                                           UserName ,
       ISNULL(display_name , u.first_name+' '+u.second_name) AS FullName ,
       l.description AS                                         Location ,
       t.description AS                                         Period ,
       c.description AS                                         Catogery ,
       ut.value AS                                              Target ,
       ut.value * 100 / ut1.value AS                            AllocatedPercentage
FROM [target_m] AS t INNER JOIN user_m AS u ON t.user_id = u.user_id
                   INNER JOIN [user_target] AS ut ON t.target_id = ut.target_id
                                                   AND
                                                   t.user_id = ut.user_id
                                                   AND
                                                   t.roster_id = ut.roster_id
                   INNER JOIN category AS c ON ut.category_id = c.category_id
                                                                       AND
                                                                       ut.category_id = c.category_id
                   INNER JOIN [target_m] AS t1 ON t1.user_id IS NULL
                                                AND
                                                t1.roster_id = t.roster_id
                   INNER JOIN [roster] AS r ON r.roster_id = t.roster_id
                   INNER JOIN [location_m] AS l ON r.location_id = l.location_id
                                                                         AND
                                                                         l.deleted = 0
                   INNER JOIN [user_target] AS ut1 ON t1.target_id = ut1.target_id
                                                    AND
                                                    ut1.category_id = ut.category_id
WHERE t1.target_id = @target_templet_id";
        public virtual DbRawSqlQuery<ViewLocationTargetDistribution> getLocationTargetAssignments(BaseTarget target)
        {
            DbRawSqlQuery<ViewLocationTargetDistribution> result;
            var target_templet_id =
                string.IsNullOrEmpty(target.TargetTemplate) ?
                new SqlParameter("@target_templet_id", SqlDbType.DateTime) { Value = DBNull.Value } :
                new SqlParameter("@target_templet_id", target.TargetTemplate);

            try
            {
                result = this.Database.SqlQuery<ViewLocationTargetDistribution>(cmdGetLocationTargetAssignments,
                    target_templet_id);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
        public virtual List<ViewLocationTargetDistribution> getLocationTargetAssignments(string target)
        {
            List<ViewLocationTargetDistribution> result;
                var target_templet_id =
                string.IsNullOrEmpty(target) ?
                new SqlParameter("@target_templet_id", SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@target_templet_id", target);

            try
            {
                result = this.Database.SqlQuery<ViewLocationTargetDistribution>(cmdGetLocationTargetAssignments,
                    target_templet_id).ToList();
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
