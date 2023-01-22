using System.Threading;
using System.Threading.Tasks;
using OSRS.Application.Models.Alchemy.Command;
using OSRS.Application.Seed.Command;
using OSRS.Application.Seed.Models.Output;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Infrastructure.Repositories;

namespace OSRS.Application.Models.Alchemy.Handler
{
    public class AlchemyCommandHandler : ICommandHandler<AddAlchemyCommand, Response>
    {
        // private readonly IAlchemyRepository _alchemyEntityRepository;
        // private readonly IDomainUnitOfWork _unitOfWork;

        // public AlchemyCommandHandler(IAlchemyRepository alchemyEntityRepository, IDomainUnitOfWork unitOfWork)
        // {
        //     _alchemyEntityRepository = alchemyEntityRepository;
        //     _unitOfWork = unitOfWork;
        // }

        // public AlchemyCommandHandler()
        // {
        //     
        // }
        public Task<Response> Handle(AddAlchemyCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}