using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.Google.Queries;
using OSRS.Application.Seed.Models.Output;
using OSRS.Domain.Service;
using OSRS.Resources.Localization;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.Google.Handler
{
    public class SearchGoogleQueryHandler :
        IQueryHandler<SearchGoogleQuery, Response<int>>
    {
        private readonly ISearchGoogleService _searchGoogleService;
        private readonly IMapper _mapper;

        public SearchGoogleQueryHandler(ISearchGoogleService searchGoogleService, IMapper mapper)
        {
            _searchGoogleService = searchGoogleService;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(SearchGoogleQuery request, CancellationToken cancellationToken)
        {
            Response<int> result;

            try
            {
                result = new Response<int>(true, 
                    _mapper.Map<int>(await _searchGoogleService.GetWordPressCategoryAsync(request.Model.Keyword, request.Model.Url))) ;
            }
            catch (Exception exc)
            {
                // await _logger.LogExceptionAsync(this, exc, null, null, cancellationToken, nameof(GetProductsStatsDashboardQuery));
                result = new(false, I18n.UnknownError);
            }

            return result;
        }
    }
}