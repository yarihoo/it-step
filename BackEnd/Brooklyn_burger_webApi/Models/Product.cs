using Internet_Market_WebApi.Data.Entities.Products;

namespace Internet_Market_WebApi.Responses
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Description { get; set; } 
        public string Image { get; set; }
        public double Price { get; set; }
        public double? SalePrice { get; set; }
        public string Subcategory { get; set; }// Category Name Here
        public bool isDelete { get; set; }
    }
}
