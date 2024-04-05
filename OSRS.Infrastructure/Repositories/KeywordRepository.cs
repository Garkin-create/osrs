using System;
using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using OSRS.Domain.Entities;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public class KeywordRepository: BaseEntityRepository<KeywordObject>, IKeywordRepository
    {
        public KeywordRepository(DomainContext context, ISystemLogger logger) : base(context, logger)
        {
        }

        public async Task<OperationResult> AddKeyword(KeywordObject keyword, CancellationToken cancellationToken = default)
        {
            OperationResult answer  = new(OperationResultType.Error, "");
            try
            {
                if (await AddAsync(keyword))
                {
                    answer = new OperationResult(OperationResultType.Success, "Success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return answer;
        }
    }
}