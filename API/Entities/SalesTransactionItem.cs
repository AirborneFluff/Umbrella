namespace API.Entities;

public class SalesTransactionItem : MongoEntity
{
    public required string Description { get; set; }
    public decimal ChargedUnitCost { get; set; }
}