using BusinessExcel.Models.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class ItemDetailsView:PaginationModel, IFilter
    {
        [Column("inventory_item_id", Order = 1)]
        public int inventory_item_id { get; set; }

        [Display(Name = "Item")]
        [Column("item_code", Order = 2)]
        public string item_code { get; set; }

        [Column("description", Order = 3)]
        public string description { get; set; }

        [Column("price", Order = 4)]
        public Nullable<decimal> price { get; set; }

        [Column("model", Order = 5)]
        public string model { get; set; }

        [Column("model_description", Order = 6)]
        public string model_description { get; set; }

        [Display(Name = "Category")]
        [Column("category_id", Order = 7)]
        public int? category_id { get; set; }

        [Column("category_description", Order = 8)]
        [Display(Name ="Category Desc.")]
        public string category_description { get; set; }

        #region IFilter_Implimentation
        public string GetFilter()
        {
            throw new NotImplementedException();
        }

        public IFilter GetFilterFor(Page page)
        {
            var filter = (ItemDetailsView)this.MemberwiseClone();
            filter.PageNumber = page.PageNumber;
            return filter;
        }

        public IFilter GetFilterFor(int page)
        {
            var filter = (ItemDetailsView)this.MemberwiseClone();
            filter.PageNumber = page;
            return filter;
        }

        public List<SqlParameter> GetSQLParameterList()
        {
            var item_code = this.item_code != null ?
                new SqlParameter("@item_code", this.item_code) :
                new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var category_id = 
                this.category_id != null?
                new SqlParameter("@category_id", this.category_id) :
                new SqlParameter("@category_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var page_number = this.PageNumber > 0 ?
                new SqlParameter("@page_number", this.PageNumber) :
                new SqlParameter("@page_number", System.Data.SqlDbType.Int) { Value = DBNull.Value };

            var page_size = this.PageSize > 0 ?
                new SqlParameter("@page_size", this.PageSize) :
                new SqlParameter("@page_size", System.Data.SqlDbType.Int) { Value = DBNull.Value };

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(item_code);
            parameterList.Add(category_id);
            parameterList.Add(page_number);
            parameterList.Add(page_size);

            return parameterList;
        }
        #endregion
    }
}