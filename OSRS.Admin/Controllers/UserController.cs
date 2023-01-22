using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OSRS.Application.Models.User.Command;
using OSRS.Application.Models.User.Model.Input;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UserController: Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("AddUser")]
        public Task<Response> AddAlchemy(AddUserInputModel model)
            => _mediator.Send(new AddUserCommand(model));
        
        
        
    }
}