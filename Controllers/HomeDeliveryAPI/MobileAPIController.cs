using DataProvider;
using DataProvider.Entities;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
namespace HomeDelivery.Controllers.API
{
    public class MobileAPIController : ApiController
    {
        [HttpGet]
        public IEnumerable<ItemModel> AllProducts(string Search, int Page, bool ExtededFilter=true, string UserName =null)
        {
            List<ItemModel> result;
            int RowCount;
            if (!ExtededFilter) {
                using (SalesManageDataContext db = new SalesManageDataContext("GSLSR"))
                {
                    result = db.getModelDetails(Search, Page, out RowCount, UserName);
                }
                return result;
            }
            if (string.IsNullOrEmpty(Search)) Search = "";
            StringBuilder searchNew = new StringBuilder("");
            char last = char.MinValue;
            if (Search.Length > 0)
                last = Search[0];

            foreach (var c in Search)
            {
                if (!char.IsNumber(last) == char.IsNumber(c))
                    searchNew.Append(" ");
                last = c;
                searchNew.Append(c);
            }

            Search = searchNew.ToString();
            
            using (SalesManageDataContext db = new SalesManageDataContext("GSLSR"))
            {
                result = db.getModelDetailsExtended(Search, Page, out RowCount, UserName);
            }
            return result;
        }

        [HttpGet]
        public IEnumerable<Brand> AllBrands()
        {

            using (SalesManageDataContext db = new SalesManageDataContext("GSLSR"))
                return db.getBrands();
        }
    }
}