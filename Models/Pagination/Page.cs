using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models.Pagination
{
    public class Page
    {
        public static Page operator ++(Page vaue) { return new Page(++vaue.PageNumber, vaue.ActivePageNumber); }

        public Page(int pageNumber, int activePageNumber)
        {
            this.PageNumber = pageNumber;
            this.ActivePageNumber = activePageNumber;
            active = ActivePageNumber == pageNumber;
        }

        public int ActivePageNumber { get; set; }

        public int PageNumber { get; set; }
        public bool active { get; set; }

        public string ActiveClass
        {
            get
            {
                return active ? "active" : "";
            }
        }
    }
}