using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OSRS.Application.Models.OpenAI.Model.Output;
using OSRS.Application.Seed.Models.Output;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.OpenAI.Query
{
    public class GetOpenAIImageQuery: IQuery<Response<ImageOutputModel>>
    {
        public string Prompt { get; set; }

        public GetOpenAIImageQuery(string prompt)
        {
            Prompt = prompt;
        }
        
    }
}