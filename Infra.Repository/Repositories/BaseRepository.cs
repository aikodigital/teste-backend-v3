﻿using Domain.Contracts.Repositories;
using Domain.Entities;
using Infra.Context;
using System.Data.Entity;

namespace Infra.Repository.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    public void Create(T entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public void Update(T entity)
    {
        Context.Update(entity);
        Context.SaveChanges();
    }

    public void Delete(T entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    public T GetById(int id)
    {
        return Context.Set<T>().Where(e => e.Id == id).First();
    }

    public List<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }
}