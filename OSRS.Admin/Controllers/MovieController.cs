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
    public class MovieController : Controller
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddMovie")]
        public Task<Response> AddAlchemy(MovieInputModel model)
            => _mediator.Send(new PushMovieCommand(model));
    }
}