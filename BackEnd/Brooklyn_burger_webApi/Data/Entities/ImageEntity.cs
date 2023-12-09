using Internet_Market_WebApi.Data.Entities.Identity;

namespace Internet_Market_WebApi.Data.Entities
{
    public class ImageEntity: BaseEntity<int>
    {
        public string ImageData { get; set; }
        public UserEntity User { get; set; }
    }
}
