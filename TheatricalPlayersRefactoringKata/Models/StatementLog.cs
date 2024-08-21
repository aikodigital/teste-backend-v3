using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class StatementLog
    {
        public int Id { get; set; }
        public DateTime DtInclusao { get; set; }
        public string Costumer { get; set; }
        public string PlayId { get; set; }
        public int Audience { get; set; }
        public decimal Amount { get; set; }
        public int Credits { get; set; }

        public static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StatementLog>().HasKey(e => e.Id);
            builder.Entity<StatementLog>().HasIndex(e => new { e.DtInclusao, e.PlayId, e.Costumer }).IsUnique();

            builder.Entity<StatementLog>()
                   .HasOne<Play>()
                   .WithMany()
                   .HasForeignKey(e => e.PlayId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
        }
    }
}
