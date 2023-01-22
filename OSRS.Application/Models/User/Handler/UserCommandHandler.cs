using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.User.Command;
using OSRS.Application.Seed.Command;
using OSRS.Application.Seed.Models.Output;
using OSRS.Domain.Entities.User;
using OSRS.Domain.Repositories;
using OSRS.Infrastructure.Repositories;

namespace OSRS.Application.Models.User.Handler
{
    public class UserCommandHandler :ICommandHandler<AddUserCommand, Response>
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountRepository _accountRepository;

        public UserCommandHandler(IMapper mapper, IUserAccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }
        public async Task<Response> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Response result = null;
            try
            {
                var account = _mapper.Map<UserAccountObject>(request.Model);
                result = _mapper.Map<Response>(await _accountRepository.AddUser(account)) ;

            }
            catch (Exception e)
            {
                result = _mapper.Map<Response>(false) ;
            }

            return result;
        }
    }
}