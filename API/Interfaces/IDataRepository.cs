using API.Entities;
using MongoDB.Bson;

namespace API.Interfaces;

public interface IDataRepository<T> where T: BaseEntity
{
    Task<T?> GetById(string id);
    Task<T?> GetById(Guid id);
    void Add(T entity);
    void Remove(T entity);
}