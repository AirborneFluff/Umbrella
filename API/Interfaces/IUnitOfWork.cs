namespace API.Interfaces;

public interface IUnitOfWork
{
    public IComponentsRepository ComponentsRepository { get; }

    Task<bool> Complete();
    bool HasChanges();
}