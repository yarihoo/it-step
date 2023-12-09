using Internet_Market_WebApi.Models;

namespace Internet_Market_WebApi.Responses
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Subcategory[] Subcategories { get; set; }
        public bool isDelete { get; set; }
    }
}
