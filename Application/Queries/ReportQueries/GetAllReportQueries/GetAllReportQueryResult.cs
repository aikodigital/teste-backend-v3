using Application.Queries.Base;
using Domain.DTOs;
using Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ReportQueries.GetAllReportQueries
{
    public class GetAllReportQueryResult : QueryResultBase
    {

        public GetAllReportQueryResult() { }
        public GetAllReportQueryResult(IEnumerable<Error> errors) : base(errors) { }
        public IEnumerable<ReportDTO> Reports { get; set; }
    }
}
