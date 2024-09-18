using Shared.Queries;
using Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Base
{
    public class QueryResultBase : IQueryResult
    {
        protected QueryResultBase() { }
        protected QueryResultBase(IEnumerable<Error> errors)
        {
            Errors = errors as IReadOnlyCollection<Error>;
        }

        public IReadOnlyCollection<Error>? Errors { get; protected set; }
        public bool Success { get => !Errors?.Any() ?? true; }
    }
}

