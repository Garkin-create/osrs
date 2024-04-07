using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace OSRS.Infrastructure.ApiClient
{
    public interface IRestApiClient
    {
        Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request, CancellationToken cancellationToken);
        // T Execute<T>(IRestRequest request, CancellationToken cancellationToken);
        // Task<byte[]> DownloadDataAsync(IRestRequest request, CancellationToken cancellationToken);
        string Provider { get; set; }
        int Retries { get; set; }
        bool Logs { get; set; }
    }
}