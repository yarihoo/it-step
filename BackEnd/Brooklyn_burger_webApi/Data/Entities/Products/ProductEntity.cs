using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Internet_Market_WebApi.Data.Entities.Products
{
    public class ProductEntity: BaseEntity<int>
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(2500)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public double? SalePrice { get; set; }
        public string Image { get; set; }
        [Required]
        public SubcategoryEntity Subcategory { get; set; }
        public virtual ICollection<BagItem> BagItems { get; set; }
    }
}
