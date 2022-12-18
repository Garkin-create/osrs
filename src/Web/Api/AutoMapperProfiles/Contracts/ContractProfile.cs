using AutoMapper;
using OSRS.Api.Controllers.v1.Contracts.Model.Input;
using OSRS.Api.Controllers.v1.Contracts.Requests;
using OSRS.Application.Contracts.Command;
using OSRS.Application.Contracts.Dto.Input;
using OSRS.Domain.Entities;
using OSRS.Domain.Entities.Contracts;

namespace OSRS.Api.AutoMapperProfiles.Contracts
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<ContractInputModel, ContractInputDto>();
            CreateMap<AddContractsRequest, AddContractCommand>();
            CreateMap<RptContracts, ContractInputDto>();
        }
    }
}