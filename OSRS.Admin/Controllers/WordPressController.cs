using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OSRS.Application.Models.Project.Model.Query;
using OSRS.Application.Models.WordPress.Model.Input;
using OSRS.Application.Models.WordPress.Model.Output;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WordPressController : Controller
    {
        private readonly IMediator _mediator;

        public WordPressController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("GetCategoryList")]
        public Task<Response<IEnumerable<CategoryListOutputModel>>> GetCategoryList(CategoryListInputModel model)
            => _mediator.Send(new CategoryListQuery(model));
        
        [HttpGet]
        [Route("GetPostList")]
        public Task<Response<IEnumerable<PostListOutputModel>>> GetPostList(PostListInputModel model)
            => _mediator.Send(new PostListQuery(model));
    }
}