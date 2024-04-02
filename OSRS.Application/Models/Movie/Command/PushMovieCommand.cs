using OSRS.Application.Models.Movie.Model.Input;
using OSRS.Application.Seed.Interfaces;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Application.Models.Movie.Command
{
    public class PushMovieCommand: MovieBaseCommand<Response>, ITraceableRequest
    {
        public PushMovieCommand(MovieInputModel model) : base(model)
        {
        }
    }
}