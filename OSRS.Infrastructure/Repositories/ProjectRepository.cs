using System;
using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using OSRS.Domain.Entities.Project;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public class ProjectRepository: BaseEntityRepository<ProjectObject>, IProjectRepository
    {
        public ProjectRepository(DomainContext context, ISystemLogger logger) : base(context, logger)
        {
        }

        public async Task<OperationResult> AddProject(ProjectObject project, CancellationToken cancellationToken = default)
        {
            OperationResult answer  = new(OperationResultType.Error, "");
            try
            {
                if (await AddAsync(project))
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