using Domain.Entites;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class TheaterPlayRepository : Repository<TheaterPlayEntity>, ITheaterPlayRepository
    {
        public TheaterPlayRepository(AppSqlLiteContext context) : base(context)
        {
        }
    }
}
