using StoreInventoryOrderAPI.Models;
using StoreInventoryOrderAPI.Models.DTOs;

namespace StoreInventoryOrderAPI.Repositories.OrderRepositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrders();
    
    Task<Order> GetOrderById(int id);
    
    Task<Order> PlaceOrder(CreateOrderDto createOrderDto);
    
    Task<Order> CancelOrder(int id);
    
    Task<IEnumerable<Order>> GetOrdersByCustomerName(string customerName);
}