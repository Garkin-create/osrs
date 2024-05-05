using System.Threading.Tasks;
using OpenAI_API.Images;
using OSRS.Core.Models.Contratos;

namespace OSRS.Domain.Service
{
    public interface IOpenIAService
    {
        Task<ImageResult> CreateImageAsync(string prompt);
        Task<string> CreateSEOArticleAsync(ArticleModelObject model);
    }
}