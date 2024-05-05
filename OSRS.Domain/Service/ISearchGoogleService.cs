using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OSRS.Domain.Service
{
    public interface ISearchGoogleService
    {
        Task<int> GetKeywordPositionAsync(string keyword, string webSite, int size,
            CancellationToken cancellationToken = default);

        Task<bool> KeywordRank(CancellationToken cancellationToken = default);

        Task<IEnumerable<string>> GetWebsForKeyword(string keyword, int size,
            CancellationToken cancellationToken = default);
    }
}