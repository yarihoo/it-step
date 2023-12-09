using AutoMapper;
using Internet_Market_WebApi.Abstract;
using Internet_Market_WebApi.Data;
using Internet_Market_WebApi.Data.Entities.Identity;
using Internet_Market_WebApi.Data.Entities.Products;
using Internet_Market_WebApi.Models;
using Internet_Market_WebApi.Responses;
using Internet_Market_WebApi.Services.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using NETCore.MailKit.Core;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Internet_Market_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly MenuService _service;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;


        public MenuController(IJwtTokenService jwtTokenService,
            UserManager<UserEntity> userManager, ILogger<MenuController> logger,
            AppEFContext context, IMapper mapper, IConfiguration configuration)
        {
            _logger = logger;
            _service = new MenuService(context);
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;

        }
        [HttpGet("/Products")]
        public IActionResult GetAllProducts()
        {
            return Ok(_mapper.Map<IEnumerable<Product>>(_service.Products.GetAllProducts()));
        }
        [HttpGet("/Deals")]
        public IActionResult GetAllDeals()//GET ALL PRODUCTS WITH SPECIAL PRICE
        {
            return Ok(_mapper.Map<IEnumerable<Product>>(_service.Products.GetAllProducts().Where(x => x.SalePrice != 0)));
        }
        [HttpGet("/FullPrice")]
        public IActionResult FullPriceProducts()//GET ALL PRODUCTS WITHOUT DISCOUNT
        {
            return Ok(_mapper.Map<IEnumerable<Product>>(_service.Products.GetAllProducts().Where(x => x.SalePrice == 0)));
        }
        [HttpGet("/Subcategories")]
        public IActionResult GetAllSubcategories()
        {
            return Ok(_mapper.Map<IEnumerable<Subcategory>>(_service.Subcategories.GetAllCategories()));
        }
        [HttpGet("/Categories")]
        public IActionResult GetAllCategories()
        {
            return Ok(_mapper.Map<IEnumerable<Category>>(_service.Categories.GetAllCategories()));
        }
        [HttpPost("/Products")]
        public IActionResult AddProduct(Product product)
        {
            _service.Products.AddProduct(new ProductEntity()
            {
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                SalePrice = product.SalePrice,
                Subcategory = _service.Subcategories.GetCategoryByName(product.Subcategory),
            });
            return Ok();
        }
        [HttpPost("/Subcategories")]
        public IActionResult AddSubcategory(Subcategory subcategory)
        {
            Product[] products = subcategory.Products;
            _service.Subcategories.AddCategory(new SubcategoryEntity()
            {
                Name = subcategory.Name,
                Category = _service.Categories.GetCategoryByName(subcategory.Category),
            });
            foreach (Product item in products)
            {
                ProductEntity product = new ProductEntity()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    SalePrice = item.SalePrice,
                    Image = item.Image,
                    Subcategory = _service.Subcategories.GetCategoryByName(subcategory.Name)
                };
                _service.Products.AddProduct(product);
            }
            return Ok();
        }
        [HttpPost("/Categories")]
        public IActionResult AddCategory(Category category)
        {
            Subcategory[] subcategories = category.Subcategories;
            _service.Categories.AddCategory(new CategoryEntity()
            {
                Name = category.Name,
            });
            foreach (Subcategory item in subcategories)
            {
                Product[] products = item.Products;
                _service.Subcategories.AddCategory(
                    new SubcategoryEntity()
                    {
                        Name = item.Name,
                        Category = _service.Categories.GetCategoryByName(category.Name)
                    });
                foreach (Product product in products)
                {
                    _service.Products.AddProduct(
                        new ProductEntity()
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            SalePrice = product.SalePrice,
                            Image = product.Image,
                            Subcategory = _service.Subcategories.GetCategoryByName(item.Name)
                        });
                }
            }
            return Ok();
        }
        [HttpPost("/DeleteProduct")]
        public IActionResult DeleteProduct(string name)
        {
            _service.Products.DeleteProduct(_service.Products.GetProductByName(name));
            return Ok();
        }
        [HttpPost("/AddToBag")]
        public async Task<IActionResult> AddToBag(AddBagItemViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Jwt);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                BagItem item = new BagItem
                {
                    Product = _service.Products.GetProductById(model.ProductId),
                    Count = model.Count,
                    User = user
                };
                
                _service.BagItems.AddToBag(item);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest("Can not add item to bag");

            }
        }
        [HttpPost("/GetBagItems")]
        public async Task<IActionResult> GetBagItems(GetBagItemsViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Jwt);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                List<BagItemViewModel> items = new List<BagItemViewModel>();
                foreach (BagItem item in _service.BagItems.GetAllItems(user))
                {
                    items.Add(new BagItemViewModel
                    {
                        Id = item.Id,
                        Product = _mapper.Map<Product>(item.Product),
                        Count = item.Count
                    });
                }
                return Ok(items);
            }
            catch(Exception e) 
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("/DeleteBagItem")]
        public async Task<IActionResult> DeleteItem(DeleteBagItemViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Jwt);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                _service.BagItems.RemoveFromBag(_service.BagItems.GetBagItemById(model.Id));
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("/ChangeBagItemCount")]
        public async Task<IActionResult> ChangeBagItemCount(ChangeBagItemCOuntViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Jwt);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                _service.BagItems.ChangeCount(_service.BagItems.GetBagItemById(model.Id), model.Count);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("/GetFullPrice")]
        public async Task<IActionResult> GetPrice(JwtTokenViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Data);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                double? price = 0;
                foreach (BagItem item in _service.BagItems.GetAllItems(user))
                {
                    if (item.Product.SalePrice != 0 && item.Product.SalePrice != null)
                    {
                        price += item.Product.SalePrice * item.Count;
                    }
                    else
                    {
                        price += item.Product.Price * item.Count;
                    }    
                }
                decimal rounded = Math.Round((decimal)price, 2);
                return Ok(rounded);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
