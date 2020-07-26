using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models.Pagination
{
    public class PaginationModel
    {
        public PaginationModel() { NumPad = true; PageSize = 20; PageNumber = 1; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string LoadingElementId { get; set; }
        public string UpdateTargetId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public bool UseAjax { get; set; }

        public bool NumPad { get; set; }
        private List<Page> _Pages;
        public List<Page> Pages { get { return GetPages(); } }
        public int TotalPages { get { return (RowCount / PageSize) + 1; } }

        public IFilter Filter;
        public List<Page> GetPages()
        {
            _Pages = new List<Page>();
            Page temp;
            int start, end, pagesCutOff = 5;
            var ceiling = Math.Ceiling((decimal)pagesCutOff / 2);
            var floor = Math.Floor((decimal)pagesCutOff / 2);

            if (TotalPages < pagesCutOff)
            {
                start = 0;
                end = TotalPages;
            }
            else if (PageNumber >= 1 && PageNumber <= ceiling)
            {
                start = 0;
                end = pagesCutOff;
            }
            else if ((PageNumber + floor) >= TotalPages)
            {
                start = (TotalPages - pagesCutOff);
                end = TotalPages;
            }
            else
            {
                start = (PageNumber - (int)ceiling);
                end = (PageNumber + (int)floor);
            }
            for (int i = start + 1; i <= end; i++)
            {
                temp = new Models.Pagination.Page(i, PageNumber);
                _Pages.Add(temp);
            }
            return _Pages;
        }
    }
}