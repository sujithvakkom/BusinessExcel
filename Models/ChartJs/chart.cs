using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models.ChartJs
{
    public partial class chart
    {
        public chart(ChartType type, data data, options options) {
            this._type = type.ToString();
            this.data = data;
            this.options = options;
        }
        void setType(ChartType type)
        {
            this._type = type.ToString();
        }
        private string _type = null;
        public string type
        {
            get { return _type == null ? ChartType.line.ToString() : _type; }
            set { _type = value; }
        }
        public data data { get; set; }
        public options options { get; set; }
    }

}