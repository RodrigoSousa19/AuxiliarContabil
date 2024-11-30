using System.Linq.Expressions;
using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuxiliarContabil.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly SqlDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(SqlDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
    }
    
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        var result = _dbSet.SingleOrDefault(p => p.Id.Equals(entity.Id));
        if (result != null)
        {
            _context.Entry(result).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var result = _dbSet.SingleOrDefault(p => p.Id.Equals(id));
        if (result != null)
        {
            _dbSet.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}