using Internet_Market_WebApi.Responses;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Internet_Market_WebApi.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Product[] Products { get; set; }
        public bool isDelete { get; set; }

    }
}
