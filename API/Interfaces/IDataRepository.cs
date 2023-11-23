using API.Entities;
using MongoDB.Bson;

namespace API.Interfaces;

public interface IDataRepository<T> where T: MongoEntity
{
    Task<T?> GetById(ObjectId id);
    void Add(T entity);
    void Remove(T entity);
}