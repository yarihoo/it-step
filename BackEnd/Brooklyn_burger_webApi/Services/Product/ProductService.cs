using Internet_Market_WebApi.Data;
using Internet_Market_WebApi.Data.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Internet_Market_WebApi.Services.Product
{
    public class ProductService
    {
        private readonly AppEFContext _context;
        public ProductService(AppEFContext context)
        {
            _context = context;
        }
        public void AddProduct(ProductEntity product)
        {
            product.DateCreated = DateTime.Now;
            _context.Products.Add(product); 
            _context.SaveChanges();
        }
        public void DeleteProduct(ProductEntity product) 
        {
            product.IsDelete = true;
            _context.SaveChanges();
        }
        public void UpdateProduct(ProductEntity product, ProductEntity value)
        {
            product = value;
            _context.SaveChanges();
        }
        public ProductEntity GetProductById(int id)
        {
            return _context.Products.Where(x => x.IsDelete == false).First(p => p.Id == id);
        }
        public ProductEntity GetProductByName(string name)
        {
            return _context.Products.Where(x => x.IsDelete == false).First(p => p.Name == name);
        }
        public List<ProductEntity> GetAllProducts()
        {
            return _context.Products.Include(y => y.Subcategory).Where(x => x.IsDelete == false).ToList();
        }
    }
}
