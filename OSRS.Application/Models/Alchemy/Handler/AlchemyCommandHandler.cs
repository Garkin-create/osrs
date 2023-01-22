using System.Threading;
using System.Threading.Tasks;
using OSRS.Application.Models.Alchemy.Command;
using OSRS.Application.Seed.Command;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Application.Models.Alchemy.Handler
{
    public class AlchemyCommandHandler : ICommandHandler<AddAlchemyCommand, Response>
    {
        public Task<Response> Handle(AddAlchemyCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}