using API.Entities;
using MongoDB.Bson;

namespace API.Interfaces;

public interface IDataRepository<T> where T: MongoEntity
{
    Task<T> GetById(ObjectId id);
    Task Insert(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}