using Application.Queries.Base;
using Domain.DTOs;
using Domain.Interfaces;
using Shared.Handles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ReportQueries.GetAllReportQueries
{
    public class GetAllReportQueryHandler (IReportRepository _IReportRepository,
        IReportCreditRepository _IReportCreditRepository) : IQueryHandler<GetAllReportQuery, GetAllReportQueryResult>
    {
        public async Task<GetAllReportQueryResult> HandleAsync(GetAllReportQuery command)
        {

           var reports = await _IReportRepository.GetAllLisAsync();

            var dtos = new List<ReportDTO>();
            foreach (var item in reports)
            {
                dtos.Add(item);
            }

            return new GetAllReportQueryResult()
            {
                Reports = dtos
            };

            throw new NotImplementedException();
        }
    }
}
