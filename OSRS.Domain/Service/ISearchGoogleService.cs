using System.Threading;
using System.Threading.Tasks;

namespace OSRS.Domain.Service
{
    public interface ISearchGoogleService
    {
        Task<int> GetWordPressCategoryAsync(string keyword, string webSite,
            CancellationToken cancellationToken = default);
    }
}