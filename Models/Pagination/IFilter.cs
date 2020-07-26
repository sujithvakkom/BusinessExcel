using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessExcel.Models.Pagination
{
    public interface IFilter
    {
        string GetFilter();

        /// <summary>
        /// Parameter can be used for processing SQL Command
        /// </summary>
        /// <returns></returns>
        List<SqlParameter> GetSQLParameterList();

        /// <summary>
        /// Return IFilter object for Pagination for specific page
        /// </summary>
        /// <param name="page">Page object for a specific page</param>
        /// <returns>Filter for _Pagination</returns>
        IFilter GetFilterFor(Page page);

        /// <summary>
        /// Return IFilter object for Pagination for specific page number
        /// </summary>
        /// <param name="page">Page Number for the page</param>
        /// <returns>Filter for _Pagination</returns>
        IFilter GetFilterFor(int page);
    }
}
