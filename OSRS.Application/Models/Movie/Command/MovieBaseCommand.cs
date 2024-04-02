using OSRS.Application.Models.Movie.Model.Input;
using OSRS.Application.Seed.Command;

namespace OSRS.Application.Models.Movie.Command
{
    public class MovieBaseCommand<T> : CommandBase<T>
    {
        public MovieInputModel Model { get; set; }

        public MovieBaseCommand(MovieInputModel model)
        {
            Model = model;
        }
    }
}