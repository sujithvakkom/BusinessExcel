
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public int UpdateItemModel(ItemDetailsView itemDet)
        {
            int isInsertUpdate = 0;
            try
            {
                const string UPDATE_MODEL = @" update model  set model.description=@model_description from sc_salesmanage_vansale.inventory_item_m item
inner join sc_salesmanage_merchant.model_m model on item.model_id=model.model_id
where inventory_item_id=@inventory_item_id";



                var model_description = itemDet.model_description != null ?
              new SqlParameter("@model_description", itemDet.model_description) :
              new SqlParameter("@model_description", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


                var inventory_item_id = itemDet.inventory_item_id != 0 ?
      new SqlParameter("@inventory_item_id", itemDet.inventory_item_id) :
      new SqlParameter("@inventory_item_id", System.Data.SqlDbType.NVarChar) { Value = 0 };





                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(model_description);
                parameterList.Add(inventory_item_id);
             


                 isInsertUpdate = this.Database.ExecuteSqlCommand(UPDATE_MODEL, parameterList.ToArray());

            }
            catch(Exception ex)
            {
                isInsertUpdate = 0;
            }

            return isInsertUpdate;
        }



        public int UpdateItemCat(ItemDetailsView itemDet)
        {
            int isInsertUpdate = 0;
            try
            {
                const string UPDATE_MODEL = @"UPDATE sc_salesmanage_vansale.inventory_item_m
  SET 
      category_id = @category_id
WHERE inventory_item_id = @inventory_item_id";



                var category_id = itemDet.category_id != null ?
              new SqlParameter("@category_id", itemDet.category_id) :
              new SqlParameter("@category_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


                var inventory_item_id = itemDet.inventory_item_id != 0 ?
      new SqlParameter("@inventory_item_id", itemDet.inventory_item_id) :
      new SqlParameter("@inventory_item_id", System.Data.SqlDbType.NVarChar) { Value = 0 };





                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(category_id);
                parameterList.Add(inventory_item_id);

                isInsertUpdate = this.Database.ExecuteSqlCommand(UPDATE_MODEL, parameterList.ToArray());

            }
            catch (Exception ex)
            {
                isInsertUpdate = 0;
            }

            return isInsertUpdate;
        }
    }
}
