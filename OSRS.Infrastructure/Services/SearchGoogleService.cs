using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using OSRS.Domain.Entities;
using OSRS.Domain.Entities.Traking;
using OSRS.Domain.Seed;
using OSRS.Domain.Service;
using OSRS.Infrastructure.ApiClient;
using OSRS.Infrastructure.Repositories;
using RestSharp;

namespace OSRS.Infrastructure.Services
{
    public class SearchGoogleService : ISearchGoogleService
    {
        private readonly IRestApiClient _requester;
        private readonly IKeywordRepository _keywordRepository;
        private readonly ITrackingRepository _trackingRepository;
        private readonly ISystemLogger _logger;

        public SearchGoogleService(IRestApiClient requester, IKeywordRepository keywordRepository, ITrackingRepository trackingRepository, ISystemLogger logger)
        {
            _requester = requester ?? throw new ArgumentNullException(nameof(requester));
            _keywordRepository = keywordRepository;
            _trackingRepository = trackingRepository;
            _logger = logger;
            _requester = requester;
            _logger = logger;
            _logger = logger;
            _requester = requester;
        }

        public async Task<bool> KeywordRank(CancellationToken cancellationToken = default)
        {
            var response = false;
            try
            {
                var keywordList = _keywordRepository.ListAll().Include(x=>x.Project);
                foreach (var keyword in keywordList.ToList())
                {
                    var t = new TrackingObject()
                    {
                        KeywordId = keyword.Id,
                        Date = DateTime.Now,
                        Position = await GetKeywordPositionAsync(keyword.Word, keyword.Project.Url, 100, cancellationToken)
                    };
                    await _trackingRepository.AddTracking(t, cancellationToken);
                }

                response = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return response;
        }
        
        public async Task<int> GetKeywordPositionAsync(string keyword, string webSite, int size, CancellationToken cancellationToken = default)
        {
            int response = 100;
            try
            {
                var request = CreateGoogleSearchRequest(keyword, size);
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
        
        public async Task<IEnumerable<string>> GetWebsForKeyword(string keyword, int size, CancellationToken cancellationToken = default)
        {
            List<string> urls = new List<string>();
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNodeCollection result = null;
            try
            {
                var request = CreateGoogleSearchRequest(keyword, size);
                var search  =  await _requester.ExecuteAsync<IRestResponse>(request, cancellationToken);
                
                if (search != null)
                {
                    // Leer el contenido de la respuesta
                    string htmlContent = search.Content;
                    htmlDoc.LoadHtml(htmlContent);
                    var nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='tF2Cxc']/div[@class='yuRUbf']/a[@href]");

                    // Utilizar una expresión regular para buscar el enlace del sitio web objetivo
                    // Regex regex = new Regex(@"<div class=""BNeawe UPmit AP7Wnd lRVwie"">([^<\s]+)(?:\s[^<]*?)?<\/div>", RegexOptions.IgnoreCase);
                    Regex regex = new Regex(@"href=""\/url\?q=([^""&]+)", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(htmlContent);
                    for (int i = 0; i < matches.Count; i++)
                    {
                        urls.Add(matches[i].Groups[1].Value);
                    }
                    
                }
               
            }
            catch (Exception exc)
            {
                await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return urls;
        }
        
        private IRestRequest CreateGoogleSearchRequest(string keyword, int size)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = "google.com",
                Path = "/search"
            };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["q"] = keyword;
            query["num"] = size.ToString();
            uriBuilder.Query = query.ToString() ?? string.Empty;
            var request = new RestRequest(uriBuilder.Uri.AbsoluteUri, Method.GET);
            return request;
        }
    }
}