using Domain.Entites;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class ReportRepository : Repository<ReportEntity>, IReportRepository
    {
        public ReportRepository(Context.AppSqlLiteContext context) : base(context)
        {
        }
        public override Task<List<ReportEntity>> GetAllLisAsync()
        {
            var result = DbSet
                .Include(p => p.ReportCredit)
                .AsNoTracking().ToListAsync();


            return result;
        }

    }
}
