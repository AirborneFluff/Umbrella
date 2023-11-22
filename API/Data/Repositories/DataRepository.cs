using API.Entities;
using API.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Data.Repositories;

public class DataRepository<T> : IDataRepository<T> where T: MongoEntity
{
    private readonly IMongoCollection<T> _collection;
    
    public DataRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<T>(typeof(T).Name);
    }
    
    public async Task<T> GetById(ObjectId id)
    {
        var filter = GetIdFilter(id);
        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task Insert(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task Update(T entity)
    {
        var filter = GetIdFilter(entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task Delete(T entity)
    {
        var filter = GetIdFilter(entity.Id);
        await _collection.DeleteOneAsync(filter);
    }

    private FilterDefinition<T> GetIdFilter(ObjectId id)
    {
        return Builders<T>.Filter.Eq("_id", id);
    }
}