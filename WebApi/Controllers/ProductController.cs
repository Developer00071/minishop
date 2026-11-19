using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _service = productService;


    [HttpGet]
    public async Task<ApiResponse<List<ProductDto>>> GetAllProductsAsync()
    {
        return await _service.GetAllProductsAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<ProductDto>> GetProductAsync(int id)
    {
        return await _service.GetProductAsync(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> AddProductAsync(ProductDto product)
    {
        return await _service.AddProductAsync(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
    {
        return await _service.DeleteProductAsync(id);
    }

    [HttpPut("{id:int}")]
    public async Task<ApiResponse<bool>> UpdateProductAsync(int id, ProductDto product)
    {
        return await _service.UpdateProductAsync(id, product);
    }
}
