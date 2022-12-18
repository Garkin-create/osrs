using System.Collections.Generic;
using OSRS.Application.Contracts.Dto.Input;
using MediatR;

namespace OSRS.Application.Contracts.Command
{
    public class AddContractCommand : IRequest<int>
    {
        public List<ContractInputDto> Data { get; set; }
        public string IdLabel { get; set; }
        public string IdAgente { get; set; }
    }
}