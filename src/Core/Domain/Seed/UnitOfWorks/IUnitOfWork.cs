using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OSRS.Domain.Seed.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public Task ExecuteInTransactionAsync(Func<CancellationToken,Task> operation, CancellationToken cancellationToken = default);
        public void ExecuteInTransaction(Func<CancellationToken,Task> operation);
        public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        public void BeginTransaction();
        public Task<bool> CommitAsync(CancellationToken cancellationToken = default, [CallerMemberName] string methodName = "");
        public bool Commit([CallerMemberName] string methodName = "");
    }
}