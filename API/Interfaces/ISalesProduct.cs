namespace API.Interfaces;

public interface ISalesProduct
{
    public string SKU { get; set; }
    public string Description { get; set; }
    public decimal UnitCost { get; set; }
}