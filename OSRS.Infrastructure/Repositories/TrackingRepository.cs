using System;
using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using OSRS.Domain.Entities.Traking;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public class TrackingRepository: BaseEntityRepository<TrackingObject>, ITrackingRepository
    {
        public TrackingRepository(DomainContext context, ISystemLogger logger) : base(context, logger)
        {
        }
        
        public async Task<OperationResult> AddTracking(TrackingObject tracking, CancellationToken cancellationToken = default)
        {
            OperationResult answer  = new(OperationResultType.Error, "");
            try
            {
                if (await AddAsync(tracking))
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