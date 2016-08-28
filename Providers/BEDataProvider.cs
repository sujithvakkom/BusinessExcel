using BusinessExcel.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers
{
    public class BEDataProvider : IBEDataProvider
    {
        public ConnectionStringSettings ConnectionStringSetting { get; private set; }

        public SqlConnection GetConnection()
        {
            this.ConnectionStringSetting = ConfigurationManager.ConnectionStrings["BusinessExcelData"];
            SqlConnection connection = new SqlConnection(this.ConnectionStringSetting.ConnectionString);
            return connection;
        }
    }
}