using Internet_Market_WebApi.Data;
using Internet_Market_WebApi.Data.Entities.Identity;
using Internet_Market_WebApi.Data.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace Internet_Market_WebApi.Services.Product
{
    public class BagService
    {
        private readonly AppEFContext _context;
        public BagService(AppEFContext context)
        { 
            _context = context;
        }
        public void AddToBag(BagItem item)
        {
            if (GetAllItems(item.User).Where(x => x.Product == item.Product).ToList().Count == 0)
            {
                item.DateCreated = DateTime.Now;
                _context.BagItems.Add(item);
            }
            else
            {
                GetAllItems(item.User).First(x => x.Product == item.Product).Count += item.Count;
            }
            _context.SaveChanges();
        }

        public void RemoveFromBag(BagItem item)
        {
            item.IsDelete = true;
            _context.SaveChanges();
        }

        public void ChangeCount(BagItem item, int count)
        {
            item.Count = count;
            _context.SaveChanges();
        }

        public BagItem GetBagItemById(int id)
        {
            return _context.BagItems.FirstOrDefault(x => x.Id == id);
        }

        public List<BagItem> GetAllItems(UserEntity user)
        {
            return _context.Users
                .Include(x => x.BagItems).ThenInclude(b => b.Product)
                .FirstOrDefault(u => u.Id == user.Id)
                .BagItems.Where(y => y.IsDelete == false).ToList();
        }


        
    }
}
