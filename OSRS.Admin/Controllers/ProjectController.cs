using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OSRS.Application.Models.Movie.Command;
using OSRS.Application.Models.Movie.Model.Input;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProjectController : Controller
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddProject")]
        public Task<Response> AddProject(ProjectInputModel model)
            => _mediator.Send(new PushProjectCommand(model));
    }
}