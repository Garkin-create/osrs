using System.Collections.Generic;
using AutoMapper;
using OSRS.Domain.Seed;
using OSRS.Infrastructure.Helper;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Application.Seed.Mapping.ValueConverters
{
    public class ResponseValueConverter<TSource, TDestination> : 
        ITypeConverter<TSource, TDestination>,
        ITypeConverter<QueryResult<TSource>, CollectionResponse<TDestination>>,
        ITypeConverter<QueryResult<TSource>, PagedResponse<TDestination>>,
        ITypeConverter<NotificationPagedResult<TSource>, CollectionResponse<TDestination>>,
        ITypeConverter<NotificationResult<TSource>, Response<TDestination>>,
        ITypeConverter<OperationResult<TSource>, Response<TDestination>>
    {
        private readonly IMapper _mapper;

        public ResponseValueConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Convert(TSource source, TDestination destination, ResolutionContext context)
        {
            throw new ($"Map not found for {typeof(TSource).Name} to {typeof(TDestination).Name}");
        }

        public CollectionResponse<TDestination> Convert(QueryResult<TSource> source, CollectionResponse<TDestination> destination, ResolutionContext context)
        {
            return new(source.Result == OSRS.Domain.Seed.OperationResultType.Success, source.Message,
                _mapper.Map<IEnumerable<TDestination>>(source.Data));
        }

        public PagedResponse<TDestination> Convert(QueryResult<TSource> source, PagedResponse<TDestination> destination, ResolutionContext context)
        {
            return new(source.Result == OSRS.Domain.Seed.OperationResultType.Success, source.Message, 
                _mapper.MapTo<IEnumerable<TDestination>>(source.Data, context.Options.Items))
            {
                PageIndex = source.PageIndex,
                PageSize = source.PageSize,
                HasNext = source.HasNext,
                HasPrevious = source.HasPrevious,
                TotalCount = source.TotalCount,
                TotalPages = source.TotalPages
            };
        }

        public CollectionResponse<TDestination> Convert(NotificationPagedResult<TSource> source, CollectionResponse<TDestination> destination, ResolutionContext context)
        {
            throw new System.NotImplementedException();
        }

        public PagedResponse<TDestination> Convert(NotificationPagedResult<TSource> source, PagedResponse<TDestination> destination, ResolutionContext context)
        {
            throw new System.NotImplementedException();
        }

        public Response<TDestination> Convert(NotificationResult<TSource> source, Response<TDestination> destination, ResolutionContext context)
        {
            throw new System.NotImplementedException();
        }

        public Response<TDestination> Convert(OperationResult<TSource> source, Response<TDestination> destination, ResolutionContext context)
        {
            return new(source.Result != OperationResultType.Error, source.Message,
                _mapper.Map<TDestination>(source.Data));
        }

    }
}