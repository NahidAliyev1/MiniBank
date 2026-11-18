using Microsoft.EntityFrameworkCore;
using MiniBank.DAL.Contexts;
using MiniBank.DAL.Entities;
using MiniBank.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.DAL.Repositories.Concretes;
public class Repository<T> : IRepository<T> where T:BaseModel
{
    private readonly DbSet<T> _dbSet;
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task CreateAsync(T item)
    {
       await _dbSet.AddAsync(item);
    }

    public void DeleteAsync(T item)
    {
        _dbSet.Remove(item);
    }

    public async Task<List<T>> GetAllAsync()
    {
       return await _dbSet.ToListAsync();
    }

    public IQueryable<T> GetAllQuery()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<T> GetByIdAsync(int id)
    {
       return await _dbSet.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void UpdateAsync(int id, T item)
    {
       
        _dbSet.Update(item);
    }
}
