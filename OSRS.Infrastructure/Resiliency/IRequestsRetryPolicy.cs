using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace OSRS.Infrastructure.Helper.Resiliency
{
    public interface IRequestsRetryPolicy<T>
    {
         IRestResponse<T> Execute(Func<IRestResponse<T>> operation);
         Task<IRestResponse<T>> Execute(Func<Task<IRestResponse<T>>> operation);
         Task<IRestResponse<T>> Execute(Func<CancellationToken, Task<IRestResponse<T>>> operation, CancellationToken cancellationToken);
    }
}
