using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.User.Command;
using OSRS.Application.Seed.Command;
using OSRS.Application.Seed.Models.Output;
using OSRS.Domain.Entities.User;
using OSRS.Domain.Repositories;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Infrastructure.Repositories;

namespace OSRS.Application.Models.User.Handler
{
    public class UserCommandHandler :ICommandHandler<AddUserCommand, Response>
    {
        private readonly IMapper _mapper;
        private readonly IDomainUnitOfWork _unitOfWork;
        private readonly IUserAccountRepository _accountRepository;
        
        public UserCommandHandler(IMapper mapper, IDomainUnitOfWork unitOfWork, IUserAccountRepository accountRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public async Task<Response> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Response result = null;
            try
            {
                var account = _mapper.Map<UserAccountObject>(request.Model);
                await _unitOfWork.ExecuteInTransactionAsync(async (cancellationToken) =>{
                    await _unitOfWork.BeginTransactionAsync(cancellationToken);
                    await _accountRepository.AddUser(account);
                    if (await _unitOfWork.CommitAsync(cancellationToken, nameof(AddUserCommand)))
                        result = new Response(true, string.Empty);
                }, cancellationToken);

            }
            catch (Exception e)
            {
                result = _mapper.Map<Response>(false) ;
            }

            return result;
        }
    }
}