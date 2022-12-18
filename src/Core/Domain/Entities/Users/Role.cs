using Microsoft.AspNetCore.Identity;

namespace OSRS.Domain.Entities.Users
{
    public class Role : IdentityRole<int>, IEntity
    {
        public string Description { get; set; }
    }
}
