namespace API.Entities;

public sealed class SalesTransaction : MongoEntity
{
    public DateTime CompleteDate { get; set; }
    public decimal TotalCost { get; set; }
    public decimal TotalAmountPaid { get; set; }
    
    public IEnumerable<SalesTransactionItem> Items { get; set; } = null!;
}