using OSRS.Domain.Entities.Users;
using System.Threading.Tasks;

namespace OSRS.Persistance.Jwt
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}
