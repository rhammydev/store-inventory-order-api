namespace StoreInventoryOrderAPI.Models;

public class Order
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    public string CustomerName { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public string Status { get; set; }
    
    public DateTime OrderAt { get; set; } = DateTime.UtcNow;
}