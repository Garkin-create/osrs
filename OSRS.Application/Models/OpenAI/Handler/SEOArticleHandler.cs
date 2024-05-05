using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.OpenAI.Query;
using OSRS.Application.Seed.Models.Output;
using OSRS.Core.Models.Contratos;
using OSRS.Domain.Entities.Post;
using OSRS.Domain.Seed;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Domain.Service;
using OSRS.Infrastructure.Repositories;
using OSRS.Resources.Localization;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.OpenAI.Handler
{
    public class SEOArticleHandler: IQueryHandler<GetSEOArticleQuery, Response>
    {
        private readonly IPostRepository _postRepository;
        private readonly IDomainUnitOfWork _domainUnitOfWork;
        private readonly IOpenIAService _openIaService;
        private readonly IScrapService _scrapService;
        private readonly ISearchGoogleService _searchGoogleService;
        private readonly IMapper _mapper;

        public SEOArticleHandler(IPostRepository postRepository, IDomainUnitOfWork domainUnitOfWork, IOpenIAService openIaService, 
            IScrapService scrapService, ISearchGoogleService searchGoogleService, IMapper mapper)
        {
            _postRepository = postRepository;
            _domainUnitOfWork = domainUnitOfWork;
            _openIaService = openIaService;
            _scrapService = scrapService;
            _searchGoogleService = searchGoogleService;
            _mapper = mapper;
        }
        public async Task<Response> Handle(GetSEOArticleQuery request, CancellationToken cancellationToken)
        {
            Response result = new Response(false);
            PostObject post = new PostObject();
            try
            {
                // Búsqueda de los primeros resultados para obtener las urls
                // var urls = await _searchGoogleService.GetWebsForKeyword(request.Url, 10);
                foreach (var url in request.Url)
                {
                    post.Url = url;
                    post.CreatedDate = DateTime.Now;
                    post.IsNew = true;
                    var sruct = await _scrapService.GetHtmlStructureFromUrl(url);
                    if (sruct != null)
                    {
                        var structure = _mapper.Map<ArticleModelObject>(sruct);
                        var content = await _openIaService.CreateSEOArticleAsync(structure);
                        if (content == null) throw new ArgumentNullException(nameof(content));
                        post.Title = structure.H1Title;
                        post.Content = content;
                    }
                    else
                    {
                        post.Title = "Ha ocurrido un error con este Url";
                    }
                    if ((await _postRepository.AddPost(post, cancellationToken)).Result
                        != OperationResultType.Success 
                        || !await _domainUnitOfWork.CommitAsync(cancellationToken, nameof(SEOArticleHandler)))
                        result = new ErrorResponse("Error");
                    else
                        result = new(true, "Successful");
                    Thread.Sleep(5000); 
                }
            }
            catch (Exception e)
            {
                result = new Response<string>(false, I18n.UnknownError);
            }

            return result;
        }
    }
}