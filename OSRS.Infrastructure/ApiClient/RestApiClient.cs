using System.Threading;
using System.Threading.Tasks;
using OSRS.Domain.Seed;
using OSRS.Infrastructure.Helper;
using OSRS.Infrastructure.Helper.Resiliency;
using RestSharp;

namespace OSRS.Infrastructure.ApiClient
{
    public class RestApiClient: IRestApiClient
    {
        private string _provider;
        private int _retries;
        private bool _logs;
        protected readonly IRestClient _client;
        protected readonly ISystemLogger _logger;
        // protected readonly IElasticRepository<APILogDio> _apiLogger;

        public RestApiClient()
        {
            // _logger = logger;/
            // _apiLogger = apiLogger;
            _client = new RestClient();
            _client.UseSerializer<RestSharperJsonSerializer>();
            // _client.UserAgent = "Cubatel.Server/1.0";
            _retries = 3;
            _logs = true;
            _provider = string.Empty;
        }
        public Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request, CancellationToken cancellationToken)
        {
            IRestResponse<T> result = null;
            var response= _retries > 0
                ? new RequestsRetryPolicy<T>(_logger, _retries).Execute(async () =>
                    await _client.ExecuteAsync<T>(request, cancellationToken))
                : _client.ExecuteAsync<T>(request, cancellationToken);
            result = response.Result;
            // if (_logs) Log<T>(request, result);
            return response;
        }
        

        public string Provider { get; set; }
        public int Retries { get; set; }
        public bool Logs { get; set; }
    }
}