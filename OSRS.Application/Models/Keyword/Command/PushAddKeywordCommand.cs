using OSRS.Application.Models.Alchemy.Model.Input;
using OSRS.Application.Seed.Interfaces;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Application.Models.Alchemy.Command
{
    public class PushAddKeywordCommand : KeywordBaseCommand<Response>, ITraceableRequest
    {
        public PushAddKeywordCommand(AddKeywordInputModel model) : base(model)
        {
        }
    }
}