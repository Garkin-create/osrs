using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using OSRS.Domain.Entities.Traking;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public interface ITrackingRepository: IEntityRepository<TrackingObject>
    {
        Task<OperationResult> AddTracking(TrackingObject tracking, CancellationToken cancellationToken = default);
    }
}