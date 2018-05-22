using BootstrapHtmlHelper.DecimalExtention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models.ChartJs
{
    public partial class data<T>
    {
        private IQueryable<T> dataSource;
        private string[] IDFields = new string[]{"id_1","id_2"};
        public data(IQueryable<T> source, string[] idFields, string selection)
        {
            this.dataSource = source;
            this.IDFields = idFields;
            this.labels = source.GroupBy(x=>  IDFields[0] ).
                Select(x => (string)IDFields[0] ).ToArray();
            var labels2 = source.GroupBy(x => IDFields[1]).
                Select(x => (string)IDFields[1]).ToArray();

            foreach (var label in labels)
            {
            }
        }
        /// <summary>
        /// X Axis labels
        /// </summary>
        public string[] labels { get; set; }

        public List<datasets> datasets { get; set; }
    }

}