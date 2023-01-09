using System;
using System.Threading;
using System.Threading.Tasks;

namespace OSRS.Infrastructure.Repositories.Resiliency
{
    public interface ISqlRetryPolicy
    {
        void Execute(Action operation);
        TResult Execute<TResult>(Func<TResult> operation);
        Task Execute(Func<Task> operation);
        Task<TResult> Execute<TResult>(Func<Task<TResult>> operation);
        Task Execute(Func<CancellationToken, Task> operation, CancellationToken cancellationToken);
        Task<TResult> Execute<TResult>(Func<CancellationToken, Task<TResult>> operation, CancellationToken cancellationToken);
    }
}
