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
        public virtual UserDetail getAuthUserDetail(string userName, string password, int group = 8)
        {
            const string SELECT_USER = @"SELECT USER_NAME,ISNULL(FULL_NAME,USER_NAME)FULL_NAME,VEHICLE_CODE,PROFILE,SIGNATURE,null SESSION
                                            FROM delivery_users a left join EMPLOYBIOMETRIC b on a.user_name = b.EMPLOYCODE
                                            WHERE GROUP_ID = @group_id
                                                  AND
                                                  user_name = @user_name
                                                  AND
                                                  password = @password";

            var _user_name = !string.IsNullOrEmpty(userName) ?
                  new SqlParameter("@user_name", userName) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _password = !string.IsNullOrEmpty(password) ?
                new SqlParameter("@password", password) :
                new SqlParameter("@password", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _group_id = new SqlParameter("@group_id", group);

            var users = this.Database.SqlQuery<UserDetail>(SELECT_USER, _user_name, _password, _group_id).ToList();
            UserDetail detail = users.Count > 0 ? users[0] : null;
            return detail;
        }
        public virtual UserDetail getUserDetail(string userName)
        {
            const string SELECT_USER = @"SELECT USER_NAME,ISNULL(FULL_NAME,USER_NAME)FULL_NAME,VEHICLE_CODE,PROFILE,SIGNATURE
                                            FROM delivery_users a left join EMPLOYBIOMETRIC b on a.user_name = b.EMPLOYCODE
                                            WHERE user_name = @user_name";

            var _user_name = !string.IsNullOrEmpty(userName) ?
                  new SqlParameter("@user_name", userName) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var users = this.Database.SqlQuery<UserDetail>(SELECT_USER, _user_name).ToList();
            UserDetail detail = users.Count > 0 ? users[0] : null;
            return detail;
        }
        public virtual UserDetail getUserDetail(string userName,String session)
        {
            const string SELECT_USER = @"
SELECT d_user.USER_NAME ,
       ISNULL(d_user.FULL_NAME , d_user.USER_NAME) AS FULL_NAME ,
       d_user.VEHICLE_CODE ,
       d_user.PROFILE ,
       bio.SIGNATURE ,
       log_sess.SESSIONID as SESSION
FROM delivery_users AS d_user INNER JOIN USERLOGINSESSIONS AS log_sess ON d_user.USER_NAME = log_sess.EMPLOYCODE
                              LEFT OUTER JOIN EMPLOYBIOMETRIC AS bio ON d_user.USER_NAME = bio.EMPLOYCODE
WHERE d_user.USER_NAME = @user_name
      AND
      log_sess.SESSIONID = @session";

            var _user_name = !string.IsNullOrEmpty(userName) ?
                  new SqlParameter("@user_name", userName) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var _session = !string.IsNullOrEmpty(session) ?
                  new SqlParameter("@session", session) :
                  new SqlParameter("@session", System.Data.SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            try
            {
                var users = this.Database.SqlQuery<UserDetail>(SELECT_USER, _user_name, _session).ToList();
                UserDetail detail = users.Count > 0 ? users[0] : null;
                return detail;
            }
            catch (Exception ex)
            {
                Debug.Assert(true, ex.Message);
            }
            return null;
        }

        public virtual string UpdateUserLogin(UserDetail user, string ConnectionString)
        {
            const string UPDATE_USER_LOGIN = @"INSERT INTO USERLOGINSESSIONS ( EMPLOYCODE ,
                                SESSIONID
                              )
OUTPUT Inserted.SESSIONID
VALUES(@emp_code,
         NEWID()
       )";

            if (!string.IsNullOrEmpty(user.user_name))
                using (SqlConnection connecton = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(UPDATE_USER_LOGIN, connecton))
                    {
                        command.Parameters.AddWithValue("@emp_code", user.user_name);
                        try
                        {
                            command.Connection.Open();
                            return command.ExecuteScalar().ToString();
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                        finally { command.Connection.Close(); }
                    }
                }
            return null;
        }
    }
}