using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace OSRS.Domain.Service
{
    public interface IScrapService
    {
        Task<HtmlNodeCollection> GetHtmlStructureFromUrl(string url);
    }
}