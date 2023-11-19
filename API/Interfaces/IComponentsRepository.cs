using API.Entities;

namespace API.Interfaces;

public interface IComponentsRepository
{
    public Task<ProductComponent?> GetById(long id);
    public void Add(ProductComponent component);
    public void Remove(ProductComponent component);
}