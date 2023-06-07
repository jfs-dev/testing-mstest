using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using project_core.Interfaces;
using project_core.Models;
using project_core_mstest.Data;

namespace project_core_mstest.Repositories;

public class ClienteRepositoryInMemory : IRepository<Cliente>
{
    private readonly AppDbContext _context;

    public ClienteRepositoryInMemory()
    {
        _context = new();
    }

    public Cliente Create(Cliente entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        _context.SaveChanges();

        return entity;
    }
 
    public Cliente Update(Cliente entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();

        return entity;
    }

    public void Delete(Cliente entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public Cliente Get(params object[] key)
    {
        return _context.Clientes.Find(key) ?? new();
    }

    public IQueryable<Cliente> GetAll()
    {
        return _context.Clientes;
    }

    public IQueryable<Cliente> GetAll(Expression<Func<Cliente, bool>> predicate)
    {
        return _context.Clientes.Where(predicate).AsQueryable();
    }
}
