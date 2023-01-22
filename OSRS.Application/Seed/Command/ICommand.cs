using MediatR;

namespace OSRS.Application.Seed.Command
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
