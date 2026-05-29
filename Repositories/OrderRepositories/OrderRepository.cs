using Microsoft.EntityFrameworkCore;
using StoreInventoryOrderAPI.Data;
using StoreInventoryOrderAPI.Models;
using StoreInventoryOrderAPI.Models.DTOs;

namespace StoreInventoryOrderAPI.Repositories.OrderRepositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        var orders = await _dbContext.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order> GetOrderById(int id)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        return order ?? throw new Exception("Order not found");
    }

    public async Task<Order> PlaceOrder(CreateOrderDto createOrderDto)
    {
       
        var product = await _dbContext.Products.FindAsync(createOrderDto.ProductId);


        if (product == null)
        {
            throw new Exception("Product not found");
        }

        if (createOrderDto.Quantity <= 0)
        {
            throw new Exception("Quantity must be greater than zero");
        }
        
        // check product quantity
        if (product.StockQuantity < createOrderDto.Quantity)
        {
            throw new Exception($"Only {product.StockQuantity} stock available");
        }

        var order = new Order()
        {
            ProductId = product.Id,
            CustomerName = createOrderDto.CustomerName,
            Quantity = createOrderDto.Quantity,
            Status = createOrderDto.Status
        };
        product.StockQuantity -= createOrderDto.Quantity;
        
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order> CancelOrder(int id)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        
        var product = await _dbContext.Products.FindAsync(order.ProductId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        
        product.StockQuantity += order.Quantity;
        
        _dbContext.Remove(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerName(string customerName)
    {
        var  orders = await _dbContext.Orders.Where(o => 
            o.CustomerName == customerName)
            .ToListAsync();
        return orders;
    }
}