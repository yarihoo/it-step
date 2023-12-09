using Internet_Market_WebApi.Responses;

namespace Internet_Market_WebApi.Models
{
    public class BagItemViewModel
    {
        public int Id { get; set; } 
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
