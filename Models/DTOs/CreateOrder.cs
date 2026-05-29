namespace StoreInventoryOrderAPI.Models.DTOs;

public class CreateOrder
{
    public int ProductId { get; set; }
    
    public string CustomerName { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public string Status { get; set; }
}