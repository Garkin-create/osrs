using AutoMapper;
using OSRS.Application.Models.Alchemy.Model.Input;
using OSRS.Application.Models.Movie.Model.Input;
using OSRS.Domain.Entities;
using OSRS.Domain.Entities.Project;

namespace OSRS.Application.Models.Keyword.MapProfile
{
    public class KeywordMapProfile: Profile
    {
        public KeywordMapProfile()
        {
            CreateMap<AddKeywordInputModel, KeywordObject>();
        }
    }
}
