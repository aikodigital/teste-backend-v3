using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    namespace Infrastructure
    {
        public class TesteBackendV3DbContextFactory : IDesignTimeDbContextFactory<TesteBackendV3DbContext>
        {
            public TesteBackendV3DbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TesteBackendV3DbContext>();
                optionsBuilder.UseSqlite("Data Source=app.db");

                return new TesteBackendV3DbContext(optionsBuilder.Options);
            }
        }
    }

}
