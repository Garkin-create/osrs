using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using OSRS.Domain.Entities.Project;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public interface IProjectRepository: IEntityRepository<ProjectObject>
    {
        Task<OperationResult> AddProject(ProjectObject project, CancellationToken cancellationToken = default);
    }
}