using AutoMapper;
using OSRS.Application.Models.Alchemy.Model.Input;
using OSRS.Application.Models.WordPress.Model.Output;
using OSRS.Core.Models.Contratos;
using OSRS.Domain.Entities;

namespace OSRS.Application.Models.WordPress.MapProfile
{
    public class WordPressMapProfile: Profile
    {
        public WordPressMapProfile()
        {
            CreateMap<CategoryModel, CategoryListOutputModel>();
        }
    }
}