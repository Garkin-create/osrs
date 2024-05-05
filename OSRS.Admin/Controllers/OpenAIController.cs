using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OSRS.Application.Models.OpenAI.Model.Input;
using OSRS.Application.Models.OpenAI.Model.Output;
using OSRS.Application.Models.OpenAI.Query;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class OpenAIController : Controller
    {
        private readonly IMediator _mediator;

        public OpenAIController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GenerateImage")]
        public Task<Response<ImageOutputModel>> GenerateImage([FromBody]string prompt)
            => _mediator.Send(new GetOpenAIImageQuery(prompt));
        [HttpGet]
        [Route("GenerateSEOArticle")]
        public Task<Response> GenerateSEOArticle([FromBody] List<string> url)
            => _mediator.Send(new GetSEOArticleQuery(url));
    }
}