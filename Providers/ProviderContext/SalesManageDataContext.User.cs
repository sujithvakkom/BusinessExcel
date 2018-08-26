
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public DbSet<UserM> User { get; set; }
        // public DbSet<Roster> RosterList { get; set; }

        //public DbSet<ItemDetails> ItemDetails { get; set; }

        public string CreateUser(CreateUser usr)
        {

          
            int isInsertUpdate = 0;

            var user_name = usr.UserName != null ?
                  new SqlParameter("@user_name", usr.UserName) :
                  new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var first_name = usr.FirstName != null ?
             new SqlParameter("@first_name", usr.FirstName) :
             new SqlParameter("@first_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var second_name = usr.SecondName != null ?
           new SqlParameter("@second_name", usr.FirstName) :
           new SqlParameter("@second_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var password = usr.Password != null ?
             new SqlParameter("@password", usr.Password) :
             new SqlParameter("@password", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            var display_name = usr.DisplayName != null ?
            new SqlParameter("@display_name", usr.FirstName) :
            new SqlParameter("@display_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            var is_active = usr.IsActive != 0 ?
     new SqlParameter("@is_active", usr.IsActive) :
     new SqlParameter("@is_active", System.Data.SqlDbType.BigInt) { Value = 1 };



      

            var email = usr.Email != null ?
           new SqlParameter("@email", usr.Email) :
           new SqlParameter("@email", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            var role_id = usr.RoleId != 0 ?
     new SqlParameter("@role_id", usr.RoleId) :
     new SqlParameter("@role_id", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };



            var contact_number = usr.ContactNumber != null ?
            new SqlParameter("@contact_no", usr.ContactNumber) :
            new SqlParameter("@contact_no", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            var emp_code_lookup = usr.EmpCodeLookup != null ?
      new SqlParameter("@emp_code_lookup", usr.EmpCodeLookup) :
      new SqlParameter("@emp_code_lookup", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            var user_group_id = usr.USERGROUPID != 0 ?
   new SqlParameter("@user_group_id", usr.USERGROUPID) :
   new SqlParameter("@user_group_id", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };



   //         var user_group_name = usr.USERGROUPID != 0 ?
   //new SqlParameter("@group_name", usr.USERGROUPNAME) :
   //new SqlParameter("@group_name", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };




            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(user_name);
            parameterList.Add(first_name);
            parameterList.Add(second_name);
            parameterList.Add(password);
            parameterList.Add(display_name);
            parameterList.Add(is_active);
            parameterList.Add(role_id);
            parameterList.Add(user_group_id);
            //parameterList.Add(user_group_name);
            parameterList.Add(emp_code_lookup);
            parameterList.Add(email);
            parameterList.Add(contact_number);


            var insert_result = string.Empty;
            var result = insert_result != null ?
                new SqlParameter("@result", insert_result) :
                new SqlParameter("@result", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            result.Direction = System.Data.ParameterDirection.Output;

            parameterList.Add(result);

            try
            {
                isInsertUpdate = this.Database.ExecuteSqlCommand("[sc_salesmanage_vansale].[create_user] @user_name,@first_name,@second_name,@password,@display_name,@is_active,@role_id,@user_group_id ,@email,@contact_no , @emp_code_lookup , @result OUTPUT", parameterList.ToArray());

              
                insert_result = result.Value.ToString();

            }
            catch (Exception ex)
            {
                isInsertUpdate = 0;
               // insert_result = "Error Occured";
            }

            return insert_result;
        }
    }
}
