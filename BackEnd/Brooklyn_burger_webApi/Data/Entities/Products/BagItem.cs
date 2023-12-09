

using Internet_Market_WebApi.Data.Entities.Identity;

namespace Internet_Market_WebApi.Data.Entities.Products
{
    public class BagItem: BaseEntity<int>
    {
        public UserEntity User { get; set; }
        public ProductEntity Product { get; set; }
        public int Count { get; set; }
    }
}
