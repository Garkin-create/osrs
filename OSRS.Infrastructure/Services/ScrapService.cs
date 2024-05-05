using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using OSRS.Domain.Seed;
using OSRS.Domain.Service;

namespace OSRS.Infrastructure.Services
{
    public class ScrapService: IScrapService
    {
        private readonly ISystemLogger _logger;

        public ScrapService(ISystemLogger logger)
        {
            _logger = logger;
        }

        public async Task<HtmlNodeCollection> GetHtmlStructureFromUrl(string url)
        {
            // Descargar el contenido HTML de la URL
            string htmlContent = string.Empty;
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNodeCollection result = null;
            List<string> documents = new List<string>();
            using (WebClient client = new WebClient())
            {
                try
                {
                    htmlContent = await client.DownloadStringTaskAsync(url);
                    htmlDoc.LoadHtml(htmlContent);
                    result = htmlDoc.DocumentNode.SelectNodes("//h1 | //h2 | //h3");
                    documents.Add(htmlContent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al descargar la página: " + ex.Message);
                }
            }

            // var tf = CalculateTF(documents);
            // var idf = CalculateIDF(documents);
            // var tfIdf = CalculateTFIDF(documents);
            return result;
        }
        
        // Método para calcular la frecuencia de término (TF)
        public Dictionary<string, Dictionary<string, double>> CalculateTF(List<string> documents)
        {
            var tfDict = new Dictionary<string, Dictionary<string, double>>();

            foreach (var document in documents)
            {
                var words = document.Split(' ');
                var wordCount = words.Length;
                var wordFreqDict = new Dictionary<string, double>();

                foreach (var word in words)
                {
                    if (!wordFreqDict.ContainsKey(word))
                        wordFreqDict[word] = 0;
                    wordFreqDict[word]++;
                }

                foreach (var word in wordFreqDict.Keys.ToList())
                {
                    wordFreqDict[word] /= wordCount;
                }

                tfDict[document] = wordFreqDict;
            }

            return tfDict;
        }
        
        public Dictionary<string, double> CalculateIDF(List<string> documents)
        {
            var idfDict = new Dictionary<string, double>();
            var totalDocuments = documents.Count;

            foreach (var document in documents)
            {
                var words = document.Split(' ').Distinct();

                foreach (var word in words)
                {
                    if (!idfDict.ContainsKey(word))
                        idfDict[word] = 0;
                    idfDict[word]++;
                }
            }

            foreach (var word in idfDict.Keys.ToList())
            {
                idfDict[word] = Math.Log10(totalDocuments / (1 + idfDict[word]));
            }

            return idfDict;
        }
        
        // Método para calcular TF-IDF
        public Dictionary<string, Dictionary<string, double>> CalculateTFIDF(List<string> documents)
        {
            var tf = CalculateTF(documents);
            var idf = CalculateIDF(documents);

            var tfidfDict = new Dictionary<string, Dictionary<string, double>>();

            foreach (var document in tf.Keys)
            {
                var tfidfInnerDict = new Dictionary<string, double>();

                foreach (var word in tf[document].Keys)
                {
                    tfidfInnerDict[word] = tf[document][word] * idf[word];
                }

                tfidfDict[document] = tfidfInnerDict;
            }

            return tfidfDict;
        }
        
    }
}