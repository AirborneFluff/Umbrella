using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public sealed class ComponentsRepository : IComponentsRepository
{
    private readonly DataContext _context;

    public ComponentsRepository(DataContext context)
    {
        _context = context;
    }
    
    public Task<ProductComponent?> GetById(long id)
    {
        return _context.Components.FirstOrDefaultAsync(pc => pc.Id == id);
    }

    public void Add(ProductComponent component)
    {
        _context.Components.Add(component);
    }

    public void Remove(ProductComponent component)
    {
        _context.Components.Remove(component);
    }
}