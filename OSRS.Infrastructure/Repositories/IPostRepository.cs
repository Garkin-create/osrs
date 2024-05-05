using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using OSRS.Domain.Entities.Post;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public interface IPostRepository: IEntityRepository<PostObject>
    {
        Task<OperationResult> AddPost(PostObject
            post, CancellationToken cancellationToken = default);
    }
}