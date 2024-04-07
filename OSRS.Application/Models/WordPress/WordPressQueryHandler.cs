using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.Project.Model.Query;
using OSRS.Application.Models.WordPress.Model.Output;
using OSRS.Application.Seed.Models.Output;
using OSRS.Domain.Service;
using OSRS.Resources.Localization;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.WordPress
{
    public class WordPressQueryHandler:
        IQueryHandler<CategoryListQuery, Response<IEnumerable<CategoryListOutputModel>>>

    {
        private readonly IWordPressService _wordPressService;
        private readonly IMapper _mapper;

        public WordPressQueryHandler(IWordPressService wordPressService, IMapper mapper)
        {
            _wordPressService = wordPressService;
            _mapper = mapper;
        }
        
        public async Task<Response<IEnumerable<CategoryListOutputModel>>> Handle(CategoryListQuery request, CancellationToken cancellationToken)
        {
            Response<IEnumerable<CategoryListOutputModel>> result;

            try
            {
                result = new Response<IEnumerable<CategoryListOutputModel>>(true, 
                    _mapper.Map<IEnumerable<CategoryListOutputModel>>(await _wordPressService.GetWordPressCategoryAsync(request.Model.Url))) ;
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