using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using OSRS.Domain.Seed;
using OSRS.Domain.Seed.UnitOfWorks;

namespace OSRS.Infrastructure.Model.Domain
{
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        protected BaseUnitOfWork(DbContext context, ISystemLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private IDbContextTransaction _transaction;
        private readonly DbContext _context;
        private readonly ISystemLogger _logger;

        public Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation,
            CancellationToken cancellationToken = default) 
            => _context.Database.CreateExecutionStrategy().ExecuteAsync(operation, cancellationToken);

        public void ExecuteInTransaction(Func<CancellationToken, Task> operation)
            =>Task.Run(async () => await ExecuteInTransactionAsync(operation));

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction ??= await _context.Database.BeginTransactionAsync(cancellationToken);
        }
        public void BeginTransaction()
        {
           Task.Run(async () => await BeginTransactionAsync());
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default, 
            [CallerMemberName] string methodName = "")
        {
            var commit = false;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                if (_transaction != null)
                    await _transaction.CommitAsync(cancellationToken);

                commit = true;
            }
            catch (Exception exc)
            {
                return false;
                // //UniqueKey Violation exceptions
                // var sqlException = exc.InnerException as SqlException;
                // if (sqlException?.Number is 2601 or 2627)
                //     return commit;
                //
                // var additionalData = new Dictionary<LogDetailsType, object>();
                // try
                // {
                //     if (exc is DbUpdateException dbExc)
                //     {
                //         var settings = new JsonSerializerSettings()
                //         {
                //             MaxDepth = 1,
                //             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //         };
                //         additionalData.Add(LogDetailsType.RequestData,
                //             dbExc.Entries.Select(p => JsonConvert.SerializeObject(p.Entity, settings)));
                //     }
                // }
                // catch (Exception e)
                // {
                //     additionalData.Add(LogDetailsType.RequestData, e.Message);
                // }
                // await _logger.LogExceptionAsync(this, exc, additionalData: additionalData, cancellationToken: cancellationToken
                //     , methodName: !string.IsNullOrEmpty(methodName) ? methodName : nameof(CommitAsync));
                // if (_transaction != null)
                //     await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                if (_transaction != null) {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
            return commit;
        }
        public bool Commit([CallerMemberName] string methodName = "")
        {
            var task = Task.Run(async () => await CommitAsync(methodName: methodName));
            return task.Result;
        }
    }
}
