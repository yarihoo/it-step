using AutoMapper;
using Internet_Market_WebApi.Data.Entities.Products;
using Internet_Market_WebApi.Models;
using Internet_Market_WebApi.Responses;

namespace Internet_Market_WebApi.Mapper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() 
        {
            CreateMap<ProductEntity, Product>()
                .ForMember(x => x.Subcategory, y => y.MapFrom(el => el.Subcategory.Name));
            CreateMap<SubcategoryEntity, Subcategory>()
                .ForMember(x => x.Category, y => y.MapFrom(el => el.Category.Name));
            CreateMap<CategoryEntity, Category>()
                .ForMember(x => x.Subcategories, y => y.MapFrom(el => el.Subcategories.ToArray()));
        }
    }
}