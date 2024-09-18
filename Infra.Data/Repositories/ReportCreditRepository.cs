using Domain.Entites;
using Domain.Interfaces;
using Infra.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class ReportCreditRepository : Repository<ReportCreditEntity>, IReportCreditRepository
    {
        public ReportCreditRepository(Context.AppSqlLiteContext context) : base(context)
        {
        }
    }
}
