using AutoMapper;
using System;
using System.Collections.Generic;

namespace OSRS.Infrastructure.Helper
{
    public static class AutoMapperExtensions
    {
        public static TDestination Map<TDestination>(this IMapper mapper, object source, ResolutionContext context)
        {
            return mapper.Map<TDestination>(source, p =>
            {
                try
                {
                    foreach (var (key, value) in context.Items)
                    {
                        if (p.Items.ContainsKey(key))
                            p.Items[key] = value;
                        else
                            p.Items.Add(key, value);
                    }
                }
                catch
                {

                }
            });
        }

        public static TDest MapTo<TDest>(this IMapper mapper, object source, IDictionary<string, object> context, Action<IMappingOperationOptions<object, TDest>> optAction = null)
        {
            return mapper.Map<TDest>(source, opts => 
            {
                if(context != null && opts.Items != null)
                    foreach (var kv in context) 
                        opts.Items.Add(kv);
                optAction?.Invoke(opts);
            });
        }
        public static TDest MapTo<TSource, TDest>(this IMapper mapper, TSource source, IDictionary<string, object> context, Action<IMappingOperationOptions<TSource, TDest>> optAction = null)
        {
            return mapper.Map<TSource, TDest>(source, opts => {
                foreach (var kv in context) opts.Items.Add(kv);
                optAction?.Invoke(opts);
            });
        }
        public static TDest MapTo<TSource, TDest>(this IMapper mapper, TSource source, TDest dest, IDictionary<string, object> context, Action<IMappingOperationOptions<TSource, TDest>> optAction = null)
        {
            return mapper.Map(source, dest, opts => {
                foreach (var kv in context) opts.Items.Add(kv);
                optAction?.Invoke(opts);
            });
        }
    }
}
