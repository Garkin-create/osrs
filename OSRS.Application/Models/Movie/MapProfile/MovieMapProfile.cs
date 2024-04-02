using AutoMapper;
using OSRS.Application.Models.Movie.Model.Input;
using OSRS.Domain.Entities.Movie;

namespace OSRS.Application.Models.Movie.MapProfile
{
    public class MovieMapProfile: Profile
    {
        public MovieMapProfile()
        {
            CreateMap<MovieInputModel, MovieObject>();
        }
    }
}