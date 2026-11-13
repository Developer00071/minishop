namespace Domain.Models;

public class StockMovements
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ChangeQty { get; set; }
    public DateTime MyProperty { get; set; } = DateTime.UtcNow;
}
