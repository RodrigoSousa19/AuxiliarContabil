using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuxiliarContabil.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly SqlDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(SqlDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public async Task UpdateAsync(T entity) => _dbSet.Update(entity);
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) _dbSet.Remove(entity);
    }
}