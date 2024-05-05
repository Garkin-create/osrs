using System.Collections.Generic;
using OSRS.Application.Models.OpenAI.Model.Input;
using OSRS.Application.Seed.Models.Output;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.OpenAI.Query
{
    public class GetSEOArticleQuery : IQuery<Response>
    {
        public List<string> Url { get; set; }

        public GetSEOArticleQuery(List<string> url)
        {
            Url = url;
        }
    }
}