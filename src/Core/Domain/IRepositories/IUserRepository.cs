using OSRS.Domain.Seed;

namespace OSRS.Domain.IRepositories
{
    // public interface IUserRepository : IRepository<User>
    // {
    //     Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
    //
    //     Task AddAsync(User user, string password, CancellationToken cancellationToken);
    //
    //     Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
    //
    //     Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    // }
    
    public interface IUserRepositoryCommand : IDomainRepository
    {
    
    }

    public interface IUserRepositoryQuery : IDomainRepository
    
    {
    
    }

    public interface IUserRepository: IUserRepositoryQuery, IUserRepositoryCommand {}
}