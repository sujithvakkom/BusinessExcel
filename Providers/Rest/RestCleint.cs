using BusinessExcel.Providers.ProviderContext.Entities;
using RestSharp;
using System.Collections.Generic;

namespace BusinessExcel.Providers.Rest
{
    public class MyRestCleint: RestClient
    {
        public MyRestCleint(string baseUrl) :base(baseUrl)
        {

        }

        public List<ItemModel> AllProducts(string Search, int Page, bool ExtededFilter)
        {
            var request = new RestRequest("api/MobileAPI/AllProducts", Method.GET);
            request.AddQueryParameter("Search", Search);
            request.AddQueryParameter("Page", string.Format("{0}", Page));
            request.AddQueryParameter("ExtededFilter", string.Format("{0}", ExtededFilter));
            var result = this.Get<List<ItemModel>>(request);
            return result.Data;
        }
    }
}