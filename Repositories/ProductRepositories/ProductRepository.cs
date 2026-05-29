using Microsoft.EntityFrameworkCore;
using StoreInventoryOrderAPI.Data;
using StoreInventoryOrderAPI.Models;
using StoreInventoryOrderAPI.Models.DTOs;

namespace StoreInventoryOrderAPI.Repositories.ProductRepositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var products = await _dbContext.Products.ToListAsync();
        return products;  
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product ?? throw new Exception($"Product with id: {id} not found");
    }

    public async Task<Product> CreateProduct(CreateProductDto createProductDto)
    {
        var productExist = await _dbContext.Products.AnyAsync(p => 
            p.Name == createProductDto.Name
            && p.Category == createProductDto.Category);
        
        if (productExist)
        {
            throw new Exception("Product already exists");
        }

        if (createProductDto.Price < 1)
        {
            throw new Exception("Price must be greater than 0");
        }

        if (createProductDto.StockQuantity < 0)
        {
            throw new Exception("StockQuantity must be greater than 0");
        }

        var product = new Product()
        {
            Name = createProductDto.Name,
            Category = createProductDto.Category,
            Price = createProductDto.Price,
            StockQuantity = createProductDto.StockQuantity
        };
        
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(int id, CreateProductDto createProductDto)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            throw new Exception($"Product with id: {id} not found");
        }
        
        if (createProductDto.Price < 1)
        {
            throw new Exception("Price must be greater than 0");
        }

        if (createProductDto.StockQuantity < 0)
        {
            throw new Exception("StockQuantity must be greater than 0");
        }
        
        var productExist = await _dbContext.Products.AnyAsync(p => 
            p.Name == createProductDto.Name 
            &&  p.Category == createProductDto.Category 
            && p.Id != id);
        if (productExist)
        {
            throw new Exception("Product already exists");
        }
        
        product.Name = createProductDto.Name;
        product.Category = createProductDto.Category;
        product.Price = createProductDto.Price;
        product.StockQuantity = createProductDto.StockQuantity;
        
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            throw new Exception($"Product with id: {id} not found");
        }
        
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetLowStockProducts()
    {
        var products = await _dbContext.Products.Where(p => p.StockQuantity < 5).ToListAsync();
        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        var products = await _dbContext.Products.Where(p => 
            p.Category.ToLower() == category.ToLower()).
            ToListAsync();
        return products;
    }
}