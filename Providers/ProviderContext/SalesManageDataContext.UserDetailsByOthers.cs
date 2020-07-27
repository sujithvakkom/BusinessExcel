
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public virtual UserDetail getUserDetailByName(string Name)
        {
            const string SELECT_USER = @"select user_name, 
                                                isnull(display_name,first_name+' '+second_name) as full_name 
                                                from [sc_salesmanage_user].[user_m] 
                                            where 
                                                first_name+' '+second_name = @name";

            var user_name = Name != null ?
                  new SqlParameter("@name", Name) :
                  new SqlParameter("@name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            UserDetail detail =
                this.Database.SqlQuery<UserDetail>(SELECT_USER, user_name).ToList()[0];
            return detail;
        }
        public virtual List<UserDetail> getUserDetailByName(string search, int Page, out int RowCount)
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
                                                "[sc_salesmanage_user].[getUserDetails] @user_name ,@page_number ,@page_size ,@row_count OUTPUT", user_name, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return items;
        }

        public virtual string getUserFullNameByID(int user_Id)
        {
            string fullName = string.Empty;

            try
            {
                if (user_Id > 0)
                {
                    const string SELECT_USER = @"select
                                                isnull(display_name,first_name+' '+second_name) as full_name 
                                                from [sc_salesmanage_user].[user_m] 
                                            where 
                                                user_id = @user_id";

                    var user_id = user_Id > 0 ?
                          new SqlParameter("@user_id", user_Id) :
                          new SqlParameter("@user_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

                    fullName = this.Database.SqlQuery<UserDetail>(SELECT_USER, user_id).ToList()[0].full_name;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fullName;
        }

        public int getParenId(int userId)
        {
            int Parent_id = 0;
            try
            {
                if (userId > 0)
                {
                    List<UserTree> userList = new List<UserTree>();

                    using (var db = new SalesManageDataContext())
                    {

                        userList = db.getUserTree();


                    }

                    Parent_id = userList.Where(c => c.user_id == userId).Select(a => a.parent_id).First();



                }
            }
            catch (Exception ex)
            {
                //throw ex;
                Parent_id = 0;
            }
            return Parent_id;
        }

        public int getParentUserId(int userId)
        {
            int user_id = 0;
            try
            {
                if (userId > 0)
                {
                    List<UserTree> userList = new List<UserTree>();

                    using (var db = new SalesManageDataContext())
                    {

                        userList = db.getUserTree();


                    }

                    int pid = userList.Where(c => c.user_id == userId).Select(a => a.parent_id).Single();
                    int left_v = userList.Where(c => c.user_id == userId).Select(a => a.left_v).Single();
                    int right_v = userList.Where(c => c.user_id == userId).Select(a => a.right_v).Single();

                    var parents = userList.Where(c => c.left_v < left_v && c.right_v > right_v).OrderBy(x => x.left_v).LastOrDefault();

                    if (parents != null)
                    {
                        user_id = parents.user_id;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                user_id = 0;
            }
            return user_id;
        }
        public bool validateExist(int newuserId, int parentId)
        {
            bool isexis = false;
            try
            {

                List<UserTree> userList = new List<UserTree>();

                using (var db = new SalesManageDataContext())
                {

                    userList = db.getUserTree();
                }

                int p_left_v = userList.Where(c => c.parent_id == parentId).Select(a => a.left_v).Single();
                int p_right_v = userList.Where(c => c.parent_id == parentId).Select(a => a.right_v).Single();
                int p_level = userList.Where(c => c.parent_id == parentId).Select(a => a.level_v).Single();

                var parents = userList.Where(c => c.left_v > p_left_v && c.right_v < p_right_v && c.user_id == newuserId && c.level_v == p_level + 1).ToList();

                if (parents.Count > 0)
                {
                    isexis = true;
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                isexis = false;
            }
            return isexis;
        }
        public int getEntity(int parentid)
        {
            int Enitity_Id = 0;
            try
            {
                if (parentid > 0)
                {
                    List<UserTree> userList = new List<UserTree>();

                    using (var db = new SalesManageDataContext())
                    {

                        userList = db.getUserTree();


                    }

                    Enitity_Id = userList.Where(c => c.parent_id == parentid).Select(a => a.entity).Single();
                    //int left_v = userList.Where(c => c.user_id == userId).Select(a => a.left_v).Single();
                    //int right_v = userList.Where(c => c.user_id == userId).Select(a => a.right_v).Single();

                    // var parents = userList.Where(c => c.left_v < left_v && c.right_v > right_v).OrderBy(x => x.left_v).LastOrDefault();

                    //if (parents != null)
                    //{
                    //    Enitity_Id = parents.entity;
                    // }
                    // else
                    //{
                    //     Enitity_Id = 1;
                    // }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                Enitity_Id = 0;
            }
            return Enitity_Id;
        }
    }
}
