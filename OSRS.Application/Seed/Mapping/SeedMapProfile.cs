using System;
using AutoMapper;
using OSRS.Application.Seed.Output;
using OSRS.Domain.Seed;
using OSRS.Domain.Seed.Paging;
using OSRS.Application.Seed.Input;
using OSRS.Application.Seed.Mapping.ValueConverters;
using OSRS.Application.Seed.Models.Output;
using OSRS.Application.Seed.Queries;
using Response = OSRS.Application.Seed.Models.Output.Response;

namespace OSRS.Application.Seed.Mapping
{
    public class SeedMapProfile : Profile
    {
        public SeedMapProfile()
        {
            #region "Base Types Maps"
            CreateMap<OperationResultType, bool>()
                .ConvertUsing(v => v != OperationResultType.Error);
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));
            #endregion

            #region "Responses"
            CreateMap<OperationResult, BaseResponse>()
                .ForMember(o => o.Success, i => { i.MapFrom(o => o.Result); });
            CreateMap<OperationResult, Models.Output.Response>()
                .ForMember(o => o.Success, i => { i.MapFrom(o => o.Result); });
            CreateMap(typeof(OperationResult<>), typeof(Response<>))
                .ConvertUsing(typeof(ResponseValueConverter<,>));
            CreateMap(typeof(NotificationResult<>), typeof(Response<>))
                .ConvertUsing(typeof(ResponseValueConverter<,>));
            
            CreateMap(typeof(QueryResult<>), typeof(PagedResponse<>))
                .ConvertUsing(typeof(ResponseValueConverter<,>));
            CreateMap(typeof(QueryResult<>), typeof(CollectionResponse<>))
                .ConvertUsing(typeof(ResponseValueConverter<,>));
            CreateMap(typeof(NotificationPagedResult<>), typeof(PagedResponse<>))
                .ConvertUsing(typeof(ResponseValueConverter<,>));
            CreateMap(typeof(NotificationPagedResult<>), typeof(CollectionResponse<>))
                .ConvertUsing(typeof(ResponseValueConverter<,>));
            
            #endregion

            CreateMap<FilteringOption, FilteringOptionInput>()
                .ReverseMap();
            CreateMap<PagingArgs, PagingArgsInput>()
                .ReverseMap();
            CreateMap<PageSearchArgs, PagingArgsInput>()
                .ReverseMap();
            CreateMap<PageSearchArgs, PageSearchArgsInput>()
                .IncludeBase<PagingArgs, PagingArgsInput>()
                .ReverseMap();
            CreateMap<FilteredBasePagedQuery, PageSearchArgs>();
            

        }
    }
}

