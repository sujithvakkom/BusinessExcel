
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

        public DbSet<UserTree> UserTree { get; set; }
        // public DbSet<Roster> RosterList { get; set; }

        //public DbSet<ItemDetails> ItemDetails { get; set; }
        public virtual List<UserTree> getUserTree()
        {
            List<UserTree> users = new List<UserTree>();
            //var description = search != null ?
            //      new SqlParameter("@filter", search) :
            //      new SqlParameter("@filter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            //int? page = null;
            //var page_size = page != null ?
            //    new SqlParameter("@page_size", page) :
            //    new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

           
            try
            {
                users = this.Database.SqlQuery<UserTree>("[db_salesmanage_user].[getUserTree]").ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return users;
        }

    }
}
