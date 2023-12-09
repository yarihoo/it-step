using System.ComponentModel.DataAnnotations;

namespace Internet_Market_WebApi.Data.Entities.Products
{
    public class CategoryEntity: BaseEntity<int>
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<SubcategoryEntity> Subcategories { get; set; }
    }
}
