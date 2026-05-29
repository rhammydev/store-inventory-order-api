using StoreInventoryOrderAPI.Models;
using StoreInventoryOrderAPI.Models.DTOs;

namespace StoreInventoryOrderAPI.Repositories.ProductRepositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    
    Task<Product> GetProductById(int id);
    
    Task<Product> CreateProduct(CreateProductDto createProductDto);
    
    Task<Product> UpdateProduct(int id, CreateProductDto createProductDto);
    
    Task<bool> DeleteProduct(int id);

    Task<IEnumerable<Product>> GetLowStockProducts();
    
    Task<IEnumerable<Product>> GetProductsByCategory(string category);
}