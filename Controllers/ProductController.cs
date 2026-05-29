using Microsoft.AspNetCore.Mvc;
using StoreInventoryOrderAPI.Models;
using StoreInventoryOrderAPI.Models.DTOs;
using StoreInventoryOrderAPI.Repositories.ProductRepositories;

namespace StoreInventoryOrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    // get all products
    [HttpGet()]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productRepository.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productRepository.GetProductById(id);
        return Ok(product);
    }

    [HttpPost()]
    public async Task<IActionResult> AddProduct(CreateProductDto createProductDto)
    {
        var product = await _productRepository.CreateProduct(createProductDto);
        return Ok(product);
    }
}