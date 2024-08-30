using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Persistence
{
    public class DBContext : DbContext
    {
        private IConfiguration _config;

        public DBContext(IConfiguration config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _config.GetConnectionString("npgServer"); 
            optionsBuilder.UseNpgsql(connectionString);
        }

    }
}
