using MiniBank.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.DAL.Repositories.Interfaces;
public interface IRepository<T> where T:BaseModel
{
    Task CreateAsync(T item);
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    void UpdateAsync(int id,T item);
    void DeleteAsync(T item);
    Task SaveChangesAsync();
    IQueryable<T> GetAllQuery();
}
