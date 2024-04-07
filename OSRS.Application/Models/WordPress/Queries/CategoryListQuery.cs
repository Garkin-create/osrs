using System.Collections.Generic;
using OSRS.Application.Models.WordPress.Model.Input;
using OSRS.Application.Models.WordPress.Model.Output;
using OSRS.Application.Seed.Models.Output;
using Stu.Cubatel.Application.Seed.Queries;

namespace OSRS.Application.Models.Project.Model.Query
{
    public class CategoryListQuery : IQuery<Response<IEnumerable<CategoryListOutputModel>>>
    {
        public CategoryListInputModel Model { get; }

        public CategoryListQuery(CategoryListInputModel model)
        {
            Model = model;
        }
        
    }
}