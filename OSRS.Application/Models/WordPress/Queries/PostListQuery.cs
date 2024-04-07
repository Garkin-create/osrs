using System.Collections.Generic;
using OSRS.Application.Models.WordPress.Model.Input;
using OSRS.Application.Models.WordPress.Model.Output;
using OSRS.Application.Seed.Models.Output;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.Project.Model.Query
{
    public class PostListQuery: IQuery<Response<IEnumerable<PostListOutputModel>>>
    {
        public PostListInputModel Model { get; }

        public PostListQuery(PostListInputModel model)
        {
            Model = model;
        }
        
    }
}