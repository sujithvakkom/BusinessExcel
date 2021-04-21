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
        public virtual string getBiometric(string userName, string signatureString)
        {
            const string SELECT_BIOMETRIC = @"
SELECT EMPLOYCODE 
FROM EMPLOYBIOMETRIC
WHERE EMPLOYCODE = @emp_code
      AND
      SIGNATURE = @sign";

            var _emp_code = !string.IsNullOrEmpty(userName) ?
                  new SqlParameter("@emp_code", userName) :
                  new SqlParameter("@emp_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _sign = !string.IsNullOrEmpty(signatureString) ?
                new SqlParameter("@sign", signatureString) :
                new SqlParameter("@sign", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var userResult = this.Database.SqlQuery<String>(SELECT_BIOMETRIC, _emp_code, _sign).First();
            return userResult;
        }

        public virtual int updateBioMetric(string userName, string signatureString)
        {
            const string INSERT_BIOMETRIC = @"INSERT INTO EMPLOYBIOMETRIC ( EMPLOYCODE ,
                              SIGNATURE
                            )
VALUES ( @emp_code ,
         @sign
       )";


            var _emp_code = !string.IsNullOrEmpty(userName) ?
                  new SqlParameter("@emp_code", userName) :
                  new SqlParameter("@emp_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _sign = !string.IsNullOrEmpty(signatureString) ?
                new SqlParameter("@sign", signatureString) :
                new SqlParameter("@sign", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            int count = 0;
            try
            {
                count = this.Database.ExecuteSqlCommand(INSERT_BIOMETRIC, _emp_code, _sign);
            }
            catch (Exception ex)
            {
                count = -1;
                Debug.Assert(true, ex.Message);
            }
            return count;
        }
    }
}