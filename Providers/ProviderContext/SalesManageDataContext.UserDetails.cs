
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public virtual UserDetail getUserDetail(string userName)
        {
            const string SELECT_USER = @"select user_name, 
                                                isnull(display_name,first_name+' '+second_name) as full_name 
                                                from [sc_salesmanage_user].[user_m] 
                                            where 
                                                user_name = @user_name";

            var user_name = userName != null ?
                  new SqlParameter("@user_name", userName) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var users = this.Database.SqlQuery<UserDetail>(SELECT_USER, user_name).ToList();
            UserDetail detail = users.Count > 0 ? users[0] : null;
            return detail;
        }
        public virtual UserDetail getAuthUserDetail(string userName, string password)
        {
            const string SELECT_USER = @"select user_name, 
                                                isnull(display_name,first_name+' '+second_name) as full_name 
                                                from [sc_salesmanage_user].[user_m] 
                                            where 
                                                user_name = @user_name
                                            and password = @password";

            var _user_name = !string.IsNullOrEmpty(userName) ?
                  new SqlParameter("@user_name", userName) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _password = !string.IsNullOrEmpty(password) ?
                new SqlParameter("@password", password) :
                new SqlParameter("@password", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var users = this.Database.SqlQuery<UserDetail>(SELECT_USER, _user_name,_password).ToList();
            UserDetail detail = users.Count > 0 ? users[0] : null;
            return detail;
        }
        public virtual int getUserID(string userName)
        {
            const string SELECT_USER = @"select user_id
                                                from [sc_salesmanage_user].[user_m] 
                                            where 
                                                user_name = @user_name";

            var user_name = userName != null ?
                  new SqlParameter("@user_name", userName) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int detail =
                this.Database.SqlQuery<int>(SELECT_USER, user_name).ToList()[0];
            return detail;
        }

        /// <summary>
        /// Retrive user id by using membership name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual int getViewer_Id()
        {

            int login_member_user_id = (int)Membership.GetUser().ProviderUserKey;

            const string SELECT_USER = @"select isnull(user_id,0)
                                                from [sc_salesmanage_user].[user_m] 
                                            where 
                                                user_membership_id = @user_membership_id";

            var user_id = login_member_user_id > 0 ?
                  new SqlParameter("@user_membership_id", login_member_user_id) :
                  new SqlParameter("@user_membership_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int vId = 0;
            try
            {
                  vId = this.Database.SqlQuery<int>(SELECT_USER, user_id).ToList()[0];
            }
            catch (Exception)
            {
                return vId = 0;
            }
            return vId;
        }
        public virtual List<int> getViewersList()
        {
            List<int> vw = new List<int>();


            int VId = getViewer_Id();

            var viewer_id = VId > 0 ?
              new SqlParameter("@viewer_id", VId) :
              new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            vw = this.Database.SqlQuery<int>(
                                            "select user_id from  db_salesmanage_user.f_getLeastChildrens_byParentUser_Id(@viewer_id)",  viewer_id)
                                            .ToList();

         
            return vw;
        }
        public virtual List<UserDetail> getUserDetails(string search, int Page, out int RowCount)
        {
            List<UserDetail> items = new List<UserDetail>();

            int VId = getViewer_Id();

            var viewer_id = VId > 0 ?
              new SqlParameter("@viewer_id", VId) :
              new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            var user_name = search != null ?
                    new SqlParameter("@user_name", search) :
                    new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;
            try
            {
                items = this.Database.SqlQuery<UserDetail>(
                                                "[sc_salesmanage_user].[getUserDetails] @user_name ,@page_number ,@page_size ,@row_count OUTPUT,@viewer_id", user_name, page_number, page_size, row_count, viewer_id)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex) {
                RowCount = 0;
            }
            return items;
        }
        public virtual List<UserDetail> getUserDetailsAll(string search, int Page, out int RowCount)
        {
            List<UserDetail> items = new List<UserDetail>();

          

            var user_name = search != null ?
                    new SqlParameter("@user_name", search) :
                    new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;
            try
            {
                items = this.Database.SqlQuery<UserDetail>(
                                                "[sc_salesmanage_user].[getUserDetailsAll] @user_name ,@page_number ,@page_size ,@row_count OUTPUT", user_name, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return items;
        }


        public virtual List<CreateUser> getUserGroups()
        {
            List<CreateUser> ugroups = new List<CreateUser>();

            const string SELECT_USER = @"select user_group_id as USERGROUPID, 
                                                group_name as USERGROUPNAME
                                                from [sc_salesmanage_user].[group_m] ";
            try
            {
                ugroups = this.Database.SqlQuery<CreateUser>(SELECT_USER).ToList();
                
            }
            catch (Exception ex)
            {
                ugroups = null;
            }
            return ugroups;
        }

        public virtual List<CreateUser> getDefaultUserGroups()
        {
            List<CreateUser> ugroups = new List<CreateUser>();

            const string SELECT_USER = @"select user_group_id as USERGROUPID, 
                                                group_name as USERGROUPNAME
                                                from [sc_salesmanage_user].[group_m]  where user_group_id=1";
            try
            {
                ugroups = this.Database.SqlQuery<CreateUser>(SELECT_USER).ToList();

            }
            catch (Exception ex)
            {
                ugroups = null;
            }
            return ugroups;
        }


        public virtual List<CreateUser> getUserRoles()
        {
            List<CreateUser> ugroups = new List<CreateUser>();

            const string SELECT_USER_ROLES = @"select role_id as RoleId, 
                                                role_name as RoleName
                                                from [db_salesmanage_user].[role_m] ";
            try
            {
                ugroups = this.Database.SqlQuery<CreateUser>(SELECT_USER_ROLES).ToList();

            }
            catch (Exception ex)
            {
                ugroups = null;
            }
            return ugroups;
        }

        public virtual List<CreateUser> getDefaultUserRoles()
        {
            List<CreateUser> ugroups = new List<CreateUser>();

            const string SELECT_USER_ROLES = @"select role_id as RoleId, 
                                                role_name as RoleName
                                                from [db_salesmanage_user].[role_m]  where role_id=1 ";
            try
            {
                ugroups = this.Database.SqlQuery<CreateUser>(SELECT_USER_ROLES).ToList();

            }
            catch (Exception ex)
            {
                ugroups = null;
            }
            return ugroups;
        }
        /// <summary>
        /// return non assinged users
        /// </summary>
        /// <param name="search"></param>
        /// <param name="Page"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        public virtual List<UserDetail> getnonAssingedUserDetails(string search, int Page, out int RowCount)
        {
            List<UserDetail> items = new List<UserDetail>();



            var user_name = search != null ?
                    new SqlParameter("@user_name", search) :
                    new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;
            try
            {
                items = this.Database.SqlQuery<UserDetail>(
                                                "[sc_salesmanage_user].[getNonAssignedUserDetails] @user_name ,@page_number ,@page_size ,@row_count OUTPUT", user_name, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return items;
        }

        /// <summary>
        /// set userid,parentid,entity id to save
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="P_id"></param>
        /// <param name="E_Id"></param>
        /// <returns></returns>
        public int InsertUserTree(string parent_user_name,string new_user_name)
        {
            var isInsertUpdate = 0;
            try
            {

                int new_userId = getUserID(new_user_name);

                int Parent_userId = getUserID(parent_user_name);

                int Parent_id = getParenId(Parent_userId);
                int Enity_id = getEntity(Parent_userId);


          

                var user_id = new_userId != 0 ?
      new SqlParameter("@user_id", new_userId) :
      new SqlParameter("@user_id", System.Data.SqlDbType.NVarChar) { Value = 0 };

                var parent_id = Parent_id != 0 ?
              new SqlParameter("@ParentId", Parent_id) :
              new SqlParameter("@ParentId", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


                var enity_id = Enity_id != 0 ?
              new SqlParameter("@EntityId", Enity_id) :
              new SqlParameter("@EntityId", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(user_id);
                parameterList.Add(parent_id);
                parameterList.Add(enity_id);

                isInsertUpdate = this.Database.ExecuteSqlCommand("[db_salesmanage_user].[Insert_UserTree] @user_id,@ParentId,@EntityId", parameterList.ToArray());

            }
            catch(Exception ex)
            {
                isInsertUpdate = 0;
            }
      
            return isInsertUpdate;
        }

        public int DeleteUserTree(string u_name)
        {
            var isDelete = 0;
            try
            {
                int userId = getUserID(u_name);
                int Parent_id = getParenId(userId);

                var parent_id = Parent_id != 0 ?
              new SqlParameter("@ParentId", Parent_id) :
              new SqlParameter("@ParentId", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


       

                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                
                parameterList.Add(parent_id);


                isDelete = this.Database.ExecuteSqlCommand("[db_salesmanage_user].[Delete_UserTree] @ParentId", parameterList.ToArray());

            }
            catch (Exception ex)
            {
                isDelete = 0;
            }

            return isDelete;
        }

    }
}
