using JogadoresTeatrais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogaresTeatrais.Data.Extensions
{
    public static class ModelBuilderEntension
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {

            builder.Entity<Jogar>()
                .HasData(
                 new Jogar { Id = 001, Nome = "Hamlet", Linhas = 4024, Tipo = "tragedy" },
                 new Jogar { Id = 002, Nome = "As You Like It", Linhas = 2670, Tipo = "comedy" },
                 new Jogar { Id = 003, Nome = "Othello", Linhas = 3560, Tipo = "tragedy" },
                 new Jogar { Id = 004, Nome = "Henry V", Linhas = 3227, Tipo = "history" },
                 new Jogar { Id = 005, Nome = "John", Linhas = 3560, Tipo = "history" },
                 new Jogar { Id = 006, Nome = "Richard-III", Linhas = 3718, Tipo = "history" }

                );

            builder.Entity<Desempenho>()
                .HasData(
                 new Desempenho { Id = 001, JogarId = 001, Audiencia = 55 },
                 new Desempenho { Id = 003, JogarId = 002, Audiencia = 35 },
                 new Desempenho { Id = 004, JogarId = 003, Audiencia = 40 }

                );

            return builder;
        }
    }
}
