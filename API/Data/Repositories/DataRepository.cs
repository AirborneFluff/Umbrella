using API.Entities;
using API.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Data.Repositories;

public class DataRepository<T> : IDataRepository<T> where T: MongoEntity
{
    private readonly DataContext _context;
    
    public DataRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<T?> GetById(ObjectId id)
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

    private FilterDefinition<T> GetIdFilter(ObjectId id)
    {
        return Builders<T>.Filter.Eq("_id", id);
    }
}