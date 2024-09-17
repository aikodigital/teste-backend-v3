using Shared.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Handles
{
    public interface IQueryHandler<TRequest, TResult> where TRequest : IQueryRequest where TResult : IQueryResult
    {
        Task<TResult> HandleAsync(TRequest command);
    }
}
