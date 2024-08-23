using Microsoft.EntityFrameworkCore;
using System;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class PlayType
    {
        public string Name { get; set; }
        public DateTime DtInclusao { get; set; }
        public string? Description { get; set; }

        public static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayType>().HasKey(p => p.Name);

            builder.Entity<PlayType>()
                   .HasData(new PlayType { Name = "tragedy", Description = null, DtInclusao = DateTime.Now },
                            new PlayType { Name = "comedy", Description = null, DtInclusao = DateTime.Now },
                            new PlayType { Name = "history", Description = "new genre", DtInclusao = DateTime.Now }
                   );
        }
    }
}
