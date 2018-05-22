using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapHtmlHelper.DecimalExtention;

namespace BusinessExcel.Models.ChartJs
{
    public partial class datasets
    {
        public datasets(string label,decimal[] data)
        {
            this.label = label;
            this.data = data;
        }
        /*X Axis values */
        public string label { get; set; }
        public decimal[] data { get; set; }
        public string[] backgroundColor { get; set; }
        public string[] borderColor { get; set; }
        public decimal _borderWidth;
        public decimal borderWidth { get { return _borderWidth<=0 ? 0 : _borderWidth; } set { _borderWidth = value; } }

        private string[] IDField;
    }

}