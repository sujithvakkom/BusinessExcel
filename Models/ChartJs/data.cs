using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models.ChartJs
{
    public partial class data
    {
        /// <summary>
        /// X Axis labels
        /// </summary>
        public string[] labels { get; set; }

        public datasets datasets { get; set; }
    }

}