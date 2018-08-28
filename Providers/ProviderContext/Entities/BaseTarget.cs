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
            LineTargets = new List<LineTarget>(6)
            {
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

        public List<LineTarget> LineTargets { get; set; }

        public static explicit operator BaseTarget(TargetMasterDetails v)
        {
            return new BaseTarget() { TargetTemplate = v.target_id.ToString(), Description = v.description };
        }

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

        private decimal? _TotalTarget;
        [Display(Name = "Total Target")]
        [DataType(DataType.Currency)]
        public decimal? TotalTarget
        {
            get
            {
                try
                {
                    _TotalTarget = this.LineTargets.Sum(x => x.Target);
                }
                catch (Exception) { _TotalTarget = 0; }
                return _TotalTarget == null ? 0 : _TotalTarget;
            }
            set { _TotalTarget = value == null ? 0 : value; }
        }

        [Display(Name = "Base Incentive")]
        [DataType(DataType.Currency)]
        public decimal? BaseIncentive { get; set; }

        public decimal? base_incentive_qtr;

        internal virtual int Save(out string Message)
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

        public static explicit operator BaseTarget(_BaseTarget v)
        {
            return new BaseTarget() {
                TargetTemplate = v.TargetTemplate.ToString(),
                Description = v.Description,
                BaseIncentive = v.BaseIncentive,
                EndDate = v.EndDate,
                Location = v.Location.ToString(),
                StartDate = v.StartDate
            };
        }
    }
}