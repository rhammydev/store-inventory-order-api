using Microsoft.EntityFrameworkCore;
using StoreInventoryOrderAPI.Models;

namespace StoreInventoryOrderAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    // Save Products to DB
    public DbSet<Product> Products { get; set; }
    
    // Save Orders to DB
    public DbSet<Order> Orders { get; set; }
}