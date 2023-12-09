using Internet_Market_WebApi.Data;
using Internet_Market_WebApi.Data.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Internet_Market_WebApi.Services.Product
{
    public class CategoryService
    {
        private readonly AppEFContext _context;
        public CategoryService(AppEFContext context)
        {
            _context = context;
        }
        public void AddCategory(CategoryEntity category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(CategoryEntity category)
        {
            category.IsDelete = true;
            _context.SaveChanges();
        }
        public void UpdateCategory(CategoryEntity category, CategoryEntity value) 
        {
            category = value;
            _context.SaveChanges();
        }
        public CategoryEntity GetCategoryById(int id)
        { 
            return _context.Categories.Include(x => x.Subcategories).ThenInclude(el => el.Products).Where(x => x.IsDelete == false).First(c => c.Id == id);
        }
        public CategoryEntity GetCategoryByName(string name)
        {
            return _context.Categories.Include(x => x.Subcategories).ThenInclude(el => el.Products).Where(x => x.IsDelete == false).First(c => c.Name == name);
        }
        public List<CategoryEntity> GetAllCategories()
        {
            return _context.Categories.Where(x => x.IsDelete == false).Include(y => y.Subcategories).ThenInclude(el => el.Products).ToList();
        }
    }
}
