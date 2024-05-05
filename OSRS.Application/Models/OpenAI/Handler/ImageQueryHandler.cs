using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.OpenAI.Model.Output;
using OSRS.Application.Models.OpenAI.Query;
using OSRS.Application.Seed.Models.Output;
using OSRS.Core.Models.Contratos;
using OSRS.Domain.Service;
using OSRS.Resources.Localization;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.OpenAI.Handler
{
    public class ImageQueryHandler: 
        IQueryHandler<GetOpenAIImageQuery, Response<ImageOutputModel>>
        
    {
        private readonly IOpenIAService _openIaService;
        private readonly IMapper _mapper;

        public ImageQueryHandler(IOpenIAService openIaService, IMapper mapper)
        {
            _openIaService = openIaService;
            _mapper = mapper;
        }
        public async Task<Response<ImageOutputModel>> Handle(GetOpenAIImageQuery request, CancellationToken cancellationToken)
        {
            Response<ImageOutputModel> result;

            try
            {
                result = new Response<ImageOutputModel>(true,_mapper.Map<ImageOutputModel>(await _openIaService.CreateImageAsync(request.Prompt))) ;
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