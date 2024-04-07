using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using OSRS.Domain.Seed;
using OSRS.Domain.Service;
using OSRS.Infrastructure.ApiClient;
using RestSharp;

namespace OSRS.Infrastructure.Services
{
    public class SearchGoogleService : ISearchGoogleService
    {
        private readonly IRestApiClient _requester;
        private readonly ISystemLogger _logger;

        public SearchGoogleService(IRestApiClient requester, ISystemLogger logger)
        {
            _requester = requester ?? throw new ArgumentNullException(nameof(requester));
            _logger = logger;
            _requester = requester;
            _logger = logger;
            _logger = logger;
            _requester = requester;
        }
        
        public async Task<int> GetWordPressCategoryAsync(string keyword, string webSite, CancellationToken cancellationToken = default)
        {
            int response = 0;
            try
            {
                var request = CreateGoogleSearchRequest(keyword);
                var search  =  await _requester.ExecuteAsync<IRestResponse>(request, cancellationToken);
                
                if (search != null)
                {
                    // Leer el contenido de la respuesta
                    string htmlContent = search.Content;

                    // Utilizar una expresión regular para buscar el enlace del sitio web objetivo
                    Regex regex = new Regex(@"<div class=""BNeawe UPmit AP7Wnd lRVwie"">([^<\s]+)(?:\s[^<]*?)?<\/div>", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(htmlContent);
                    
                    for (int i = 0; i < matches.Count; i++)
                    {
                        // Obtener la URL del enlace
                        string url = matches[i].Groups[1].Value;

                        // Verificar si la URL contiene el sitio web objetivo
                        if (url.Contains(webSite))
                        {
                            response = i + 1;
                            break;
                        }
                    }
                }
               
            }
            catch (Exception exc)
            {
                await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return response;
        }
        
        private IRestRequest CreateGoogleSearchRequest(string keyword)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = "google.com",
                Path = "/search"
            };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["q"] = keyword;
            query["num"] = "100";
            uriBuilder.Query = query.ToString() ?? string.Empty;
            var request = new RestRequest(uriBuilder.Uri.AbsoluteUri, Method.GET);
            return request;
        }
    }
}