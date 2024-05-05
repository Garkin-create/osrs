using System;
using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using OSRS.Domain.Entities.Post;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public class PostRepository: BaseEntityRepository<PostObject>, IPostRepository
    {
        public PostRepository(DomainContext context, ISystemLogger logger) : base(context, logger)
        {
        }

        public async Task<OperationResult> AddPost(PostObject post, CancellationToken cancellationToken = default)
        {
            OperationResult answer  = new(OperationResultType.Error, "");
            try
            {
                if (await AddAsync(post))
                {
                    answer = new OperationResult(OperationResultType.Success, "Success");
                }
            }
            catch (Exception e)
            {
                answer  = new(OperationResultType.Error, "");
            }
            return answer;
        }
    }
}