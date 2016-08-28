using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.Entity
{
    public class JobList:List<JobList>,IJob
    {
        public string ControllerName { get; set; }

        public string DisplayName { get; set; }

        public string ViewName { get; set; }
    }
}