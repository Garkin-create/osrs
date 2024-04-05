using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.Alchemy.Command;
using OSRS.Application.Models.Movie.Command;
using OSRS.Application.Seed.Command;
using OSRS.Application.Seed.Models.Output;
using OSRS.Domain.Entities;
using OSRS.Domain.Entities.Project;
using OSRS.Domain.Seed;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Infrastructure.Repositories;

namespace OSRS.Application.Models.Alchemy.Handler
{
    public class KeywordCommandHandler : ICommandHandler<PushAddKeywordCommand, Response>
    {
        private readonly IKeywordRepository _keywordRepository;
        private readonly IDomainUnitOfWork _domainUnitOfWork;
        private readonly IMapper _mapper;

        public KeywordCommandHandler(IKeywordRepository keywordRepository,  IDomainUnitOfWork domainUnitOfWork, IMapper mapper)
        {
            _keywordRepository = keywordRepository;
            _domainUnitOfWork = domainUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(PushAddKeywordCommand request, CancellationToken cancellationToken)
        {
            Response result;
            KeywordObject project = _mapper.Map<KeywordObject>(request.Model);
            try
            {
                if ((await _keywordRepository.AddKeyword(project, cancellationToken)).Result
                    != OperationResultType.Success 
                    || !await _domainUnitOfWork.CommitAsync(cancellationToken, nameof(PushAddKeywordCommand)))
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