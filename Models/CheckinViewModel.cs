using BusinessExcel.Models.Pagination;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class CheckinViewModel : PaginationModel, IFilter
    {
        [Display(Name = "Shift Date")]
        public DateTime shift_date { get; set; }

        public int user_id { get; set; }

        [Display(Name = "Staff Name")]
        public string user_name { get; set; }

        public string fullname { get; set; }

        public DateTime? checkin_time { get; set; }

        public Decimal? checkin_latitude { get; set; }

        public Decimal? checkin_longitude { get; set; }
        public string checkin_address { get; set; }

        public DateTime? checkout_time { get; set; }

        public Decimal? checkout_latitude { get; set; }

        public Decimal? checkout_longitude { get; set; }
        public string checkout_address { get; set; }

        public int? breaks { get; set; }

        public int? break_span { get; set; }

        public int? row_count { get; set; }

        public string GetFilter()
        {
            throw new NotImplementedException();
        }

        public IFilter GetFilterFor(Page page)
        {
            var filter = (CheckinViewModel)this.MemberwiseClone();
            filter.PageNumber = page.PageNumber;
            return filter;
        }

        public IFilter GetFilterFor(int page)
        {
            var filter = (CheckinViewModel)this.MemberwiseClone();
            filter.PageNumber = page;
            return filter;
        }

        public List<SqlParameter> GetSQLParameterList()
        {
            throw new NotImplementedException();
        }
    }
}