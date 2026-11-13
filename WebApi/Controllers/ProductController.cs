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
    public async Task<ApiResponse<List<ProductDto>>> GetAllProducts()
    {

        return await _service.GetAllProducts();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<ProductDto>> GetProduct(int id)
    {

        return await _service.GetProduct(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> AddProduct(ProductDto product)
    {

        return await _service.AddProduct(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> DeleteProduct(int id)
    {

        return await _service.DeleteProduct(id);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> UpdateProduct(ProductDto product)
    {

        return await _service.UpdateProduct(product);
    }
}
