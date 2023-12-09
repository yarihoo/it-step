namespace Internet_Market_WebApi.Data.Entities.Products
{
    public class SubcategoryEntity: BaseEntity<int>
    {
        public CategoryEntity Category { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
