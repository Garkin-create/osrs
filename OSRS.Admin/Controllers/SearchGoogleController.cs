using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OSRS.Application.Models.Google.Model.Input;
using OSRS.Application.Models.Google.Queries;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class SearchGoogleController: Controller
    {
        private readonly IMediator _mediator;

        public SearchGoogleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("GetGooglePosition")]
        public Task<Response<int>> GetGooglePosition([FromBody] SearchGoogleInputModel model)
            => _mediator.Send(new SearchGoogleQuery(model));
        
    }
}