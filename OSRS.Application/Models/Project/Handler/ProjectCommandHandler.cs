using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.Movie.Command;
using OSRS.Application.Seed.Command;
using OSRS.Application.Seed.Models.Output;
using OSRS.Domain.Entities.Project;
using OSRS.Domain.Seed;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Infrastructure.Repositories;

namespace OSRS.Application.Models.Movie.Handler
{
    public class ProjectCommandHandler: ICommandHandler<PushProjectCommand, Response>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IDomainUnitOfWork _domainUnitOfWork;
        private readonly IMapper _mapper;

        public ProjectCommandHandler(IProjectRepository projectRepository, IDomainUnitOfWork domainUnitOfWork, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _domainUnitOfWork = domainUnitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Response> Handle(PushProjectCommand request, CancellationToken cancellationToken)
        {
            Response result;
            ProjectObject project = _mapper.Map<ProjectObject>(request.Model);
            try
            {
                if ((await _projectRepository.AddProject(project, cancellationToken)).Result
                    != OperationResultType.Success 
                    || !await _domainUnitOfWork.CommitAsync(cancellationToken, nameof(PushProjectCommand)))
                    result = new ErrorResponse("Error");
                else
                    result = new(true, "Successful");
            }
            catch (Exception exc)
            {
                result = new ErrorResponse("Error");
            }

            return result;
        }
    }
}