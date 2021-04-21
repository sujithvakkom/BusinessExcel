using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Entities;
using System.Data.SqlClient;
using DataProvider.Entities.DeliveryJob;

namespace DataProvider
{

    public partial class SalesManageDataContext : DbContext
    {
        public SalesManageDataContext()
            : base("SalesManageData")
        {
            Database.SetInitializer<SalesManageDataContext>(null);
        }
        public SalesManageDataContext(String ConnectionString)
            : base(ConnectionString)
        {
            Database.SetInitializer<SalesManageDataContext>(null);
        }

        public List<string> getRetailers()
        {
            return
            Database.SqlQuery<String>("select [name] from [dbo].[delivery_retailer] order by [name] desc").ToList();
        }
        public bool Log(string request, string jsonParameter)
        {
            #region QUERY
            var cmd = @"INSERT INTO REQUESTLOG ( REQUEST ,
                         PARAMETER
                       )
VALUES ( @Request ,
         @Parameter
       )";
            #endregion
            var _Request = !string.IsNullOrEmpty(request) ?
                  new SqlParameter("@Request", request) :
                  new SqlParameter("@Request", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _Parameter = !string.IsNullOrEmpty(jsonParameter) ?
                new SqlParameter("@Parameter", jsonParameter) :
                new SqlParameter("@Parameter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            return 0 < Database.ExecuteSqlCommand(cmd, _Request, _Parameter);
        }

        public void LogError(Exception ex)
        {
            #region QUERY
            string cmd = @"INSERT INTO [dbo].[ERRORS] ( [ERRORMESSAGEID] ,
                             [DESCRIPTION],
                             [CODEUNIT]
                           )
VALUES(@ErrorMessageId,
         @Description,
         @CodeUnit
       )";
            #endregion
            int code = ex.HResult;
            string message = ex.Message.Length > 250 ? ex.Message.Substring(0, 250) : ex.Message;
            string codeUnit = ex.StackTrace.ToString().Length > 250 ? ex.StackTrace.ToString().Substring(0, 250) : ex.StackTrace.ToString();
            var _ErrorMessageId =
                  new SqlParameter("@ErrorMessageId", code);
            var _Description = !string.IsNullOrEmpty(message) ?
                new SqlParameter("@Description", message) :
                new SqlParameter("@Description", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _CodeUnit = !string.IsNullOrEmpty(codeUnit) ?
                new SqlParameter("@CodeUnit", codeUnit) :
                new SqlParameter("@CodeUnit", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            Database.ExecuteSqlCommand(cmd, _ErrorMessageId, _Description, _CodeUnit);
        }
    }
}