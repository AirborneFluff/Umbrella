using API.Entities;
using API.Interfaces;

namespace API.Data.Repositories;

public class DataRepository<T> : IDataRepository<T> where T: BaseEntity
{
    private readonly DataContext _context;
    
    public DataRepository(DataContext context)
    {
        _context = context;
    }

    public Task<T?> GetById(string id)
    {
        return GetById(Guid.Parse(id));
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _context.FindAsync<T>(id);
    }

    public void Add(T entity)
    {
        _context.Add(entity);
    }

    public void Remove(T entity)
    {
        _context.Remove(entity);
    }
}