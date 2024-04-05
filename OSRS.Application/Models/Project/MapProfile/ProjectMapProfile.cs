using AutoMapper;
using OSRS.Application.Models.Movie.Model.Input;
using OSRS.Domain.Entities.Project;

namespace OSRS.Application.Models.Movie.MapProfile
{
    public class ProjectMapProfile: Profile
    {
        public ProjectMapProfile()
        {
            CreateMap<ProjectInputModel, ProjectObject>();
        }
    }
}