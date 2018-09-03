
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using BootstrapHtmlHelper.ChartJs;
using System.Data.SqlClient;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        internal IQueryable<graph> GetGraphForCurrentMonthQuantity(int viewer, DateTime startDate, DateTime endDate)
        {


            var viewer_id = new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = viewer };

            string cmd =
                                               @"SELECT x.category Label,x.category XValues,
       cast(SUM(quantity) as decimal(18,2)) Value
FROM [sc_salesmanage_merchant].[daily_update_v] AS x INNER JOIN db_salesmanage_user.f_getLeastChildrens_byParentUser_Id ( {0}
                                                                                                                        ) AS y ON x.user_id = y.user_id
																														and create_time between '{1}' and '{2}'
group by x.category";
            var result = this.Database.SqlQuery<graph>(
                string.Format(cmd,viewer.ToString(),startDate.ToString("ddMMMyyyy"),endDate.ToString("ddMMMyyyy"))
                                               )
                                                .AsQueryable<graph>();

            return result;
        }

        internal IQueryable<graph> GetGraphForCurrentMonthValue(int viewer, DateTime startDate, DateTime endDate)
        {


            var viewer_id = new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = viewer };

            string cmd = @"SELECT x.category Label,x.category XValues,
       cast(SUM(value) as decimal(18,2)) Value
FROM [sc_salesmanage_merchant].[daily_update_v] AS x INNER JOIN db_salesmanage_user.f_getLeastChildrens_byParentUser_Id ( {0}
                                                                                                                        ) AS y ON x.user_id = y.user_id
																														and create_time between '{1}' and '{2}'
group by x.category";
            var result = this.Database.SqlQuery<graph>(
                string.Format(cmd, viewer.ToString(), startDate.ToString("ddMMMyyyy"), endDate.ToString("ddMMMyyyy"))
                                               )
                                                .AsQueryable<graph>();

            return result;
        }

        public IQueryable<GraphData> GetDataForCurrentMonthValue(int viewer, DateTime startDate, DateTime endDate)
        {


            var viewer_id = new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = viewer };

            string cmd = @"SELECT x.user_name AS   ""User"" ,
       x.name AS        UserName ,
       x.item AS        Item ,
       x.description AS ItemDescription ,
       CAST(SUM(value) as decimal(18,2))  AS TotalValue
FROM[sc_salesmanage_merchant].[daily_update_v] AS x INNER JOIN db_salesmanage_user.f_getLeastChildrens_byParentUser_Id( {0}
                                                                                                                        ) AS y ON x.user_id = y.user_id
																														and create_time between '{1}' and '{2}'
GROUP BY x.user_name ,
       x.name ,
       x.item ,
       x.description";
            var result = this.Database.SqlQuery<GraphData>(
                string.Format(cmd, viewer.ToString(), startDate.ToString("ddMMMyyyy"), endDate.ToString("ddMMMyyyy"))
                                               )
                                                .AsQueryable<GraphData>();

            return result;
        }

        public IQueryable<GraphData> GetDataForCurrentMonthQuantity(int viewer, DateTime startDate, DateTime endDate)
        {


            var viewer_id = new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = viewer };

            string cmd = @"SELECT x.user_name AS   ""User"" ,
       x.name AS        UserName ,
       x.item AS        Item ,
       x.description AS ItemDescription ,
       CAST(SUM(quantity) as decimal(18,2))  AS TotalValue
FROM[sc_salesmanage_merchant].[daily_update_v] AS x INNER JOIN db_salesmanage_user.f_getLeastChildrens_byParentUser_Id( {0}
                                                                                                                        ) AS y ON x.user_id = y.user_id
																														and create_time between '{1}' and '{2}'
GROUP BY x.user_name ,
       x.name ,
       x.item ,
       x.description";
            var result = this.Database.SqlQuery<GraphData>(
                string.Format(cmd, viewer.ToString(), startDate.ToString("ddMMMyyyy"), endDate.ToString("ddMMMyyyy"))
                                               )
                                                .AsQueryable<GraphData>();

            return result;
        }
    }
}
