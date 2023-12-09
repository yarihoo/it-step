using Internet_Market_WebApi.Data;
using Microsoft.AspNetCore.CookiePolicy;

namespace Internet_Market_WebApi.Services.Product
{
    public class MenuService
    {
        public ProductService Products { get; set; }
        public CategoryService Categories { get; set; }
        public SubcategoriesService Subcategories { get; set; }
        public BagService BagItems { get; set; }
        public MenuService(AppEFContext context)
        { 
            Products = new ProductService(context);
            Categories = new CategoryService(context);
            Subcategories = new SubcategoriesService(context);
            BagItems = new BagService(context);
        }

    }
}
