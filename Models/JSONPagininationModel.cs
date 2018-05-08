using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class JSONPagininationModel<T>
    {
        public int? CountCurrPage { get; set; }
        public int? CountPerPage { get; set; }
        public int? Count { get; set; }
        
        private List<T> _OutputList;
        public List<T> OutputList { get { return _OutputList; } set { _OutputList = value; CountCurrPage = _OutputList.Count; } }
    }
}