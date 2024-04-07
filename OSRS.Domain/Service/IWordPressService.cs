using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OSRS.Core.Models.Contratos;

namespace OSRS.Domain.Service
{
    public interface IWordPressService
    {
        Task<IEnumerable<CategoryModel>> GetWordPressCategoryAsync(string url,
            CancellationToken cancellationToken = default);
    }
}