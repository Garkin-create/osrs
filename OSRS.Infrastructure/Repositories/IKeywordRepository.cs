using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using OSRS.Domain.Entities;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public interface IKeywordRepository: IEntityRepository<KeywordObject>
    {
        Task<OperationResult> AddKeyword(KeywordObject keyword, CancellationToken cancellationToken = default);
    }
}