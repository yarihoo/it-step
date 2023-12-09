using Internet_Market_WebApi.Data;
using Internet_Market_WebApi.Data.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Internet_Market_WebApi.Services.Product
{
    public class SubcategoriesService
    {
        private readonly AppEFContext _context;
        public SubcategoriesService(AppEFContext context)
        {
            _context = context;
        }
        public void AddCategory(SubcategoryEntity category)
        {
            _context.Subcategories.Add(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(SubcategoryEntity category)
        {
            category.IsDelete = true;
            _context.SaveChanges();
        }
        public void UpdateCategory(SubcategoryEntity category, SubcategoryEntity value) 
        {
            category = value;
            _context.SaveChanges();
        }
        public SubcategoryEntity GetCategoryById(int id)
        { 
            return _context.Subcategories.Include(x => x.Products).Include(el => el.Category).First(c => c.Id == id);
        }
        public SubcategoryEntity GetCategoryByName(string name)
        {
            return _context.Subcategories.Include(x => x.Products).Include(el => el.Category).First(c => c.Name == name);
        }
        public List<SubcategoryEntity> GetAllCategories()
        {
            return _context.Subcategories.Where(x => x.IsDelete == false).Include(y => y.Products).Include(el => el.Category).ToList();
        }
    }
}
