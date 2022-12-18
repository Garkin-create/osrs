using MediatR;

namespace OSRS.Application.Seed.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}