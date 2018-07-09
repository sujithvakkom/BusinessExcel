using BootstrapHtmlHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class BaseTarget
    {
        public BaseTarget() {
        }

        public BaseTarget(bool check)
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
            this.Description = null;
            this.BaseIncentive = null;
            this.TargetTemplate = null;
        }

        public LineTarget[] LineTargets { get; set; }

        [Display(Name = "Target Template")]
        public string TargetTemplate { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Site")]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Total Target")]
        [DataType(DataType.Currency)]
        public Decimal TotalTarget { get; set; }

        [Display(Name = "Base Incentive")]
        [DataType(DataType.Currency)]
        public decimal? BaseIncentive { get; set; }

        internal int Save(out string Message)
        {
            using (var db = new SalesManageDataContext())
            {
                return db.createUpdateTarget(this,out Message);
            }
        }
        /*
         * [category] [nvarchar](10) NULL,
         * [is_bonus] [bit] NOT NULL,
         * [target_val]*/
        internal DataTable getTargetLine()
        {
            var data =
                from x in this.LineTargets
                where (decimal)(x.Target == null ? 0 : x.Target) != 0
                select new { category = x.Catogery, is_bonus = Convert.ToInt32(x.IsBonusLine), target_val = (decimal)(x.Target == null ? 0 : x.Target) };
            return data.ToList().ToDataTable();
        }
    }
}