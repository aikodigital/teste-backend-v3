using Application.Queries.ReportQueries.GetAllReportQueries;
using Application.Services.Interfaces;
using Domain.DTOs;
using Shared.Handles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReportService(IQueryHandler<GetAllReportQuery, GetAllReportQueryResult> _QueryGetAllReportHandler) : IReportService
    {
        public async Task<IEnumerable<ReportDTO>> GetAll()
        {

          var result = await _QueryGetAllReportHandler.HandleAsync(new GetAllReportQuery());
          return result.Reports;
        }
    }
}
