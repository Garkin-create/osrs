using OSRS.Application.Models.Movie.Model.Input;
using OSRS.Application.Seed.Interfaces;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Application.Models.Movie.Command
{
    public class PushProjectCommand: ProjectBaseCommand<Response>, ITraceableRequest
    {
        public PushProjectCommand(ProjectInputModel model) : base(model)
        {
        }
    }
}