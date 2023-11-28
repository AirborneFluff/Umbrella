namespace API.Entities;

public sealed class SalesOrder : MongoEntity
{
    public IEnumerable<SalesTransaction> Transactions { get; set; } = null!;
}