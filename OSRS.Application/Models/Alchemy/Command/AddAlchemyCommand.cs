using OSRS.Application.Models.Alchemy.Model.Input;
using OSRS.Application.Seed.Interfaces;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Application.Models.Alchemy.Command
{
    public class AddAlchemyCommand : AlchemyBaseCommand<Response>, ITraceableRequest
    {
        public AddAlchemyCommand(AddAlchemyInputModel model) : base(model)
        {
        }
    }
}