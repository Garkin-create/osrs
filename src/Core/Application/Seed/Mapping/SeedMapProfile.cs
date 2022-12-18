using System;
using AutoMapper;
using OSRS.Application.Models.Seed.Output;
using OSRS.Domain;
using OSRS.Domain.Seed;

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
            CreateMap<OperationResult, Response>()
                .ForMember(o => o.Success, i => { i.MapFrom(o => o.Result); });
                // .ForMember(o => o.Notification, i => i.MapFrom<NotificationValueResolver>());
            // CreateMap(typeof(OperationResult<>), typeof(Response<>))
            //     .ConvertUsing(typeof(ResponseValueConverter<,>));
            // CreateMap<NotificationResult, Response>()
            //     .ForMember(o => o.Success, i => { i.MapFrom(o => o.Result); })
            //     .ForMember(o => o.Notification, i => i.MapFrom<NotificationValueResolver>());
            // CreateMap(typeof(NotificationResult<>), typeof(Response<>))
            //     .ConvertUsing(typeof(ResponseValueConverter<,>));
            //
            // CreateMap(typeof(QueryResult<>), typeof(PagedResponse<>))
            //     .ConvertUsing(typeof(ResponseValueConverter<,>));
            // CreateMap(typeof(QueryResult<>), typeof(CollectionResponse<>))
            //     .ConvertUsing(typeof(ResponseValueConverter<,>));
            // CreateMap(typeof(NotificationPagedResult<>), typeof(PagedResponse<>))
            //     .ConvertUsing(typeof(ResponseValueConverter<,>));
            // CreateMap(typeof(NotificationPagedResult<>), typeof(CollectionResponse<>))
            //     .ConvertUsing(typeof(ResponseValueConverter<,>));
            //
            // #region "Configuration Responses"
            // CreateMap(typeof(OperationResult<>), typeof(ConfigurationResponse<>))
            //     .ForMember("Success", i => { i.MapFrom(o => ((OperationResult)o).Result); })
            //     .ForMember("Notification", i => { i.MapFrom<NotificationValueResolver>(); })
            //     .ForMember("Configuration", i => { i.MapFrom<ConfigurationValueResolver>(); });
            //
            // CreateMap(typeof(QueryResult<>), typeof(ConfigurationPagedResponse<>))
            //     .ForMember("Success", i => { i.MapFrom(o => ((OperationResult)o).Result); })
            //     .ForMember("Notification", i => { i.MapFrom<NotificationValueResolver>(); })
            //     .ForMember("Configuration", i => { i.MapFrom<ConfigurationValueResolver>(); });
             #endregion
            // #endregion
            //
            // CreateMap<FilteringOption, FilteringOptionInput>()
            //     .ReverseMap();
            // CreateMap<PagingArgs, PagingArgsInput>()
            //     .ReverseMap();
            // CreateMap<PageSearchArgs, PageSearchArgsInput>()
            //     .IncludeBase<PagingArgs, PagingArgsInput>()
            //     .ReverseMap();
            // CreateMap<FilteredBasePagedQuery, PageSearchArgs>();
            //
            // CreateMap<NotificationItem, NotificationItemModel>()
            //     .ForMember(p => p.Color, p => p.MapFrom<NotificationColorValueResolver>());
            // CreateMap<string, CustomTextItemModel>()
            //     .ConstructUsing(v => new(v, FontStyle.Normal, string.Empty));
            // CreateMap<IpAddressValue, IpAddressModel>();
            //
            // #region "Picture Models"
            // CreateMap<MediaObject, SmallPictureModel>()
            //     .ConvertUsing<MediaMappingResolver<SmallPictureModel>>();
            // CreateMap<MediaObject, MediumPictureModel>()
            //     .ConvertUsing<MediaMappingResolver<MediumPictureModel>>();
            // CreateMap<MediaObject, LargePictureModel>()
            //     .ConvertUsing<MediaMappingResolver<LargePictureModel>>();
            // CreateMap<MediaObject, WidePictureModel>()
            //     .ConvertUsing<MediaMappingResolver<WidePictureModel>>();
            // #endregion
        }
    }
}

