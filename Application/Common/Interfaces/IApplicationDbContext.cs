using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Invoice> Invoices { get; }
        DbSet<Performance> Performances { get; }
        DbSet<Play> Plays { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
