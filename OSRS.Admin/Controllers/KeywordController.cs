using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OSRS.Application.Models.Alchemy.Command;
using OSRS.Application.Models.Alchemy.Model.Input;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class KeywordController: Controller
    {
        private readonly IMediator _mediator;

        public KeywordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddKeyword")]
        public Task<Response> AddKeyword(AddKeywordInputModel model)
            => _mediator.Send(new PushAddKeywordCommand(model));

    }
}