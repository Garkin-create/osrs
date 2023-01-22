using AutoMapper;
using OSRS.Application.Models.User.Model.Input;
using OSRS.Domain.Entities.User;

namespace OSRS.Application.Models.User.MapProfile
{
    public class UserAccountMapProfile : Profile
    {
        public UserAccountMapProfile()
        {
            CreateMap<AddUserInputModel, UserAccountObject>()
                .ForMember(dest => dest.Email, opt =>
                    opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt =>
                    opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt =>
                    opt.MapFrom(src => src.Password));
        }
    }
}