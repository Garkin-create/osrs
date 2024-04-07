using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OSRS.Domain.Seed;
using Polly.Retry;
using Polly;

namespace OSRS.Infrastructure.Helper.Resiliency
{
    public class SqlRetryPolicy : ISqlRetryPolicy
    {
        private const int Retries = 3;
        private readonly List<int> _transientDbErrors = new() {
            SqlRetryError.Deadlock,
            SqlRetryError.Timeout,
            SqlRetryError.OpenConnectionFail,
            SqlRetryError.TransportFail,
            SqlRetryError.NetworkProblem
        };
        private readonly Policy _retryPolicy;
        private readonly AsyncRetryPolicy _retryPolicyAsync;
        private readonly ISystemLogger _logger;
        public SqlRetryPolicy(ISystemLogger logger)
        {
            _logger = logger;

            _retryPolicy = Policy
                .Handle<SqlException>(ex => _transientDbErrors.Contains(ex.Number))
                .WaitAndRetry(
                    retryCount: Retries,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: LogRetryAction);

            _retryPolicyAsync = Policy
            .Handle<SqlException>(ex => _transientDbErrors.Contains(ex.Number))
            .WaitAndRetryAsync(
                retryCount: Retries,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: LogRetryAction
            );
        }

        private void LogRetryAction(Exception exception, TimeSpan sleepTime, int reattemptCount, Context context)
        {
            string ms;
            if (exception is SqlException sqlException)
                ms = $"Transient DB Failure while executing query, error number: {sqlException.Number}; Retrying in {sleepTime} / {reattemptCount} / {Retries}";
            else
                ms = $"Transient DB Failure while executing query; Retrying in {sleepTime} / {reattemptCount} / {Retries}";
            //_logger.LogException(this, exception, ms);
            _logger.Log(this, LogType.Alert, ms);
        }

        public void Execute(Action operation)
        {
            _retryPolicy.Execute(operation.Invoke);
        }

        public TResult Execute<TResult>(Func<TResult> operation)
        {
            return _retryPolicy.Execute(operation.Invoke);
        }
        public async Task Execute(Func<Task> operation)
        {
            await _retryPolicyAsync.ExecuteAsync(operation.Invoke);
        }

        public async Task<TResult> Execute<TResult>(Func<Task<TResult>> operation)
        {
            return await _retryPolicyAsync.ExecuteAsync(operation.Invoke);
        }

        public async Task Execute(Func<CancellationToken, Task> operation, CancellationToken cancellationToken)
        {
            await _retryPolicyAsync.ExecuteAsync(operation.Invoke, cancellationToken);
        }

        public async Task<TResult> Execute<TResult>(Func<CancellationToken, Task<TResult>> operation, CancellationToken cancellationToken)
        {
            return await _retryPolicyAsync.ExecuteAsync(operation.Invoke, cancellationToken);
        }

    }
}
