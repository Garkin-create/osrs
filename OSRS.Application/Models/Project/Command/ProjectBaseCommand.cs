using OSRS.Application.Models.Movie.Model.Input;
using OSRS.Application.Seed.Command;

namespace OSRS.Application.Models.Movie.Command
{
    public class ProjectBaseCommand<T> : CommandBase<T>
    {
        public ProjectInputModel Model { get; set; }

        public ProjectBaseCommand(ProjectInputModel model)
        {
            Model = model;
        }
    }
}