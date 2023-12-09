using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Internet_Market_WebApi.Data.Entities.Identity
{
    public class RoleEntity : IdentityRole<int>
    {
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
