
namespace DataProvider.Entities
{
    public class ItemModel
    {
        public int inventory_item_id { get; set; }
        public int model_id { get; set; }
        public string item_code { get; set; }
        public string description { get; set; }
        public string model_description { get; set; }
        public string model { get; set; }
        public decimal price { get; set; }
    }

    public class Brand {
        public string BrandID { get; set; }
        public string BrandCode { get; set; }
    }
}