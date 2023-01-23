using System;
using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using Microsoft.AspNetCore.Identity;
using OSRS.Domain.Entities.User;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public class UserAccountRepository : BaseEntityRepository<UserAccountObject>, IEntityRepository<UserAccountObject>, IUserAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserAccountRepository(DomainContext domainContext, ISystemLogger logger, UserManager<IdentityUser> userManager) :base (domainContext, logger) //, UserManager<IdentityUser> userManager): base(domainContext, logger)
        {
            _userManager = userManager;
        }
        
        public async Task<bool> AddUser(UserAccountObject userAccount, CancellationToken cancellationToken = default)
        {
            var result = false;
            try
            {
                result = (await _userManager.CreateAsync(
                    new IdentityUser() {UserName = userAccount.UserName, Email = userAccount.Email},
                    userAccount.Password)).Succeeded;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return result;
        }
    }
    
    public interface IUserAccountRepository 
    {
        public Task<bool> AddUser(UserAccountObject userAccount, CancellationToken cancellationToken = default);
    }
}