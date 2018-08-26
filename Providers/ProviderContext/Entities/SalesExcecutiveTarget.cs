using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class SalesExcecutiveTarget:BaseTarget
    {
        public SalesExcecutiveTarget()
        {
            LineTargets = new LineTarget[] {
                new LineTarget(),
                new LineTarget(),
                new LineTarget(),
                new LineTarget(),
                new LineTarget(),
                new LineTarget()
            };
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
        }
        

    }
}