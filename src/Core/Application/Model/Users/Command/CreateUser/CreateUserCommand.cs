﻿using OSRS.Domain.Entities.Users;
using MediatR;

namespace OSRS.Application.Users.Command.CreateUser
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public GenderType Gender { get; set; }
    }
}
