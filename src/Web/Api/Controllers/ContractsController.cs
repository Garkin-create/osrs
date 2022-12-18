using System.Threading.Tasks;
using OSRS.Api.Controllers.v1;
using OSRS.Api.Controllers.v1.Contracts.Requests;
using OSRS.ApiFramework.Tools;
using OSRS.Application.Contracts.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OSRS.Controller
{
    [Route("[controller]")]
    public class ContractsController: BaseController
    {
        private readonly IMediator _mediator;

        public ContractsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [SwaggerOperation("add a List o contracs")]
        public async Task<ApiResult<int>> AddAsync([FromBody] AddContractsRequest request)
        {
            return new ApiResult<int>(await _mediator.Send(Mapper.Map<AddContractsRequest, AddContractCommand>(request)));
        }
    }
}