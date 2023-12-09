using Internet_Market_WebApi.Data.Entities.Products;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Internet_Market_WebApi.Data.Entities.Identity
{
    public class UserEntity : IdentityUser<int>
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
        public virtual ICollection<BagItem> BagItems { get; set; }
        public virtual ICollection<ImageEntity> Images { get; set; }

    }
}
