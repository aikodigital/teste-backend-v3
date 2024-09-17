using System;
using TP.Domain.Entities;
using TP.Infrastructure.Repositories;

public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    Invoice GetInvoiceById(Guid id);
}