using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessExcel.Providers.Interfaces
{
    interface IBEDataProvider
    {
        SqlConnection GetConnection();
    }
}
