using MediatR;
using OSRS.Application.Seed.Models.Output;

namespace Stu.Cubatel.Application.Seed.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }

    public interface IPagedQuery<TResult> : IQuery<PagedResponse<TResult>>
    {
    }

    public interface IConfigurationPagedQuery<TResult> : IQuery<ConfigurationPagedResponse<TResult>>
    {
    }


    public interface ICollectionQuery<TResult> : IQuery<CollectionResponse<TResult>>
    {
    }
}
