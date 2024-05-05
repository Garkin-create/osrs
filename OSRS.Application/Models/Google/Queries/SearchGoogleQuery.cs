using OSRS.Application.Models.Google.Model.Input;
using OSRS.Application.Seed.Models.Output;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.Google.Queries
{
    public class SearchGoogleQuery: IQuery<Response<int>>
    {
        public SearchGoogleInputModel Model { get; set; }
        public SearchGoogleQuery(SearchGoogleInputModel model)
        {
            Model = model;
        }
    }
}