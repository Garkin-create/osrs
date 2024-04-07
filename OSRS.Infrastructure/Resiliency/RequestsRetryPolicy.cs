using System;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OSRS.Domain.Seed;
using Polly.Retry;
using Polly;
using RestSharp;

namespace OSRS.Infrastructure.Helper.Resiliency
{
    public class RequestsRetryPolicy<T>: IRequestsRetryPolicy<T>
    {
        private readonly List<HttpStatusCode> _statusCodes = new() {
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.BadRequest
        };
        private readonly Policy<IRestResponse<T>> _retryPolicy;
        private readonly AsyncRetryPolicy<IRestResponse<T>> _retryPolicyAsync;
        private readonly ISystemLogger _logger;
        private readonly int _retries;
        public RequestsRetryPolicy(ISystemLogger logger, int retries = 3)
        {
            _retries = retries;
            _logger = logger;
            _retryPolicy=Policy.HandleResult<IRestResponse<T>>(message => _statusCodes.Contains(message.StatusCode))
                .WaitAndRetry(
                    retryCount: _retries,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: LogRetryAction);

            _retryPolicyAsync = Policy
            .HandleResult<IRestResponse<T>>(message => _statusCodes.Contains(message.StatusCode))
            .WaitAndRetryAsync(
                retryCount: _retries,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: LogRetryAction
            );
        }

        private void LogRetryAction(DelegateResult<IRestResponse<T>> result, TimeSpan sleepTime, int reattemptCount, Context context)
        {
            var ms = $"Request failed, status: {result.Result.StatusCode}; Retrying in {sleepTime} / {reattemptCount} / {_retries}";
            _logger.Log(this, LogType.Alert, ms);
        }

        public IRestResponse<T> Execute(Func<IRestResponse<T>> operation)
        {
            return _retryPolicy.Execute(operation.Invoke);
        }

        public async Task<IRestResponse<T>> Execute(Func<Task<IRestResponse<T>>> operation)
        {
            return await _retryPolicyAsync.ExecuteAsync(operation.Invoke);
        }

        public async Task<IRestResponse<T>> Execute(Func<CancellationToken, Task<IRestResponse<T>>> operation, CancellationToken cancellationToken)
        {
            return await _retryPolicyAsync.ExecuteAsync(operation.Invoke, cancellationToken);
        }

    }
}
