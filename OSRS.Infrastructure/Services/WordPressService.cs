using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<IEnumerable<PostModel>> GetWordPressPostAsync(string url, int page, int itemPerPage,  CancellationToken cancellationToken = default)
        {
            IEnumerable<PostModel> response = null;
            try
            {
                var request = CreateGetPostRequest(url, page, itemPerPage, Method.GET);
                var requester =  await _requester.ExecuteAsync<IEnumerable<PostModel>>(request, cancellationToken);
                if (requester != null)
                {
                    response = JsonConvert.DeserializeObject<IEnumerable<PostModel>>(requester.Content);
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
        private IRestRequest CreateGetPostRequest(string url, int page, int itemPerPage, Method method)
        {
            var uri = BuildGetPostUri(url, page, itemPerPage);
            var request = new RestRequest(uri.AbsoluteUri, method);
            return request;
        }
        
        private Uri BuildGetPostUri(string url, int page, int itemsPerPage)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = url,
                Path = "/wp-json/wp/v2/posts"
            };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["page"] = page.ToString();
            query["per_page"] = itemsPerPage.ToString();
            uriBuilder.Query = query.ToString() ?? string.Empty;
            return uriBuilder.Uri;
        }
    }
}