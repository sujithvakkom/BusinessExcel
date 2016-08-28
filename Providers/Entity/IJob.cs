using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.Entity
{
    public interface IJob
    {
        string DisplayName { get; set; }

        string ControllerName { get; set; }

        string ViewName { get; set; }
    }

    public class Job : IJob
    {
        public string ControllerName { get; set; }

        public string DisplayName { get; set; }

        public string ViewName { get; set; }
    }
}