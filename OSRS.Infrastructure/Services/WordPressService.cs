using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSRS.Core.Models.Contratos;
using OSRS.Domain.Seed;
using OSRS.Domain.Service;
using OSRS.Infrastructure.ApiClient;
using RestSharp;

namespace OSRS.Infrastructure.Services
{
    public class WordPressService: IWordPressService
    {
        private readonly IRestApiClient _requester;
        private readonly ISystemLogger _logger;

        public WordPressService(IRestApiClient requester, ISystemLogger logger)
        {
            _requester = requester ?? throw new ArgumentNullException(nameof(requester));
            _logger = logger;
            _logger = logger;
            _requester = requester;
        }
        
        public async Task<IEnumerable<CategoryModel>> GetWordPressCategoryAsync(string url, CancellationToken cancellationToken = default)
        {
            IEnumerable<CategoryModel> response = null;
            try
            {
                var request = CreateGetCategoriesRequest(url, Method.GET);
                var requester =  await _requester.ExecuteAsync<IEnumerable<CategoryModel>>(request, cancellationToken);
                if (requester != null)
                {
                    response = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(requester.Content);
                }
               
            }
            catch (Exception exc)
            {
                await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return response;
        }
        
        private IRestRequest CreateGetCategoriesRequest(string url, Method method)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = url,
                Path = "/wp-json/wp/v2/categories"
            };
            var request = new RestRequest(uriBuilder.Uri.AbsoluteUri, method);
            return request;
        }
    }
}