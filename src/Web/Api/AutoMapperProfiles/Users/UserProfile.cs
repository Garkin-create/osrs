using AutoMapper;
using OSRS.Api.Controllers.v1.Users.Requests;
using OSRS.Application.Users.Command.CreateUser;
using OSRS.Application.Users.Command.Login;

namespace OSRS.Api.AutoMapperProfiles.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SingUpRequest, CreateUserCommand>();

            CreateMap<LoginRequest, LoginCommand>();
        }
    }
}
