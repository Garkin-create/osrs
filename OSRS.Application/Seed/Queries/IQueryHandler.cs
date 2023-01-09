using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stu.Cubatel.Application.Seed.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IRequest<TResult>
    {

    }
}
