using OSRS.Common;
using OSRS.Common.Utilities;
using OSRS.Domain.Entities.Users;
using OSRS.Domain.IRepositories;
using OSRS.Persistance.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OSRS.Persistance.Helper;

namespace OSRS.Persistance.Repositories
{
    public class UserRepository: 
        BaseEntityRepository<User>,
        IUserRepository,
        IEntityRepository<User>
    {
        private IEntityRepository<User> _entityRepositoryImplementation;

        public UserRepository(OSRSWriteDbContext dbContext, ISystemLogger logger) : base(dbContext, logger)
        {
        }
    }
    // public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    // {
    //     public UserRepository(OSRSWriteDbContext dbContext)
    //         : base(dbContext)
    //     {
    //     }
    //
    //     public Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
    //     {
    //         var passwordHash = SecurityHelper.GetSha256Hash(password);
    //         return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
    //     }
    //
    //     public Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
    //     {
    //         //user.SecurityStamp = Guid.NewGuid();
    //         return UpdateAsync(user, cancellationToken);
    //     }
    //
    //     public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
    //     {
    //         user.LastLoginDate = DateTime.Now;
    //         return UpdateAsync(user, cancellationToken);
    //     }
    //
    //     public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
    //     {
    //         var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
    //         if (exists)
    //             throw new CleanArchAppException("نام کاربری تکراری است");
    //
    //         var passwordHash = SecurityHelper.GetSha256Hash(password);
    //         user.PasswordHash = passwordHash;
    //         await base.AddAsync(user, cancellationToken);
    //     }
    // }
}
