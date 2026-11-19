using System.Net;
using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _repository = productRepository;

    public async Task<ApiResponse<bool>> AddProductAsync(ProductDto product)
    {
        var model = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId
        };
        var res = await _repository.AddProductAsync(model);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Added!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not Added!", false);
    }

    public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
    {
        var res = await _repository.DeleteProductAsync(id);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Deleted!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not Deleted!", false);
    }

    public async Task<ApiResponse<List<ProductDto>>> GetAllProductsAsync()
    {
        var res = await _repository.GetAllProductsAsync();
        var products = res.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quantity = p.Quantity,
            CategoryId = p.CategoryId
        }).ToList();
        return new ApiResponse<List<ProductDto>>(HttpStatusCode.OK, "Products retrieved successfully", products);
    }

    public async Task<ApiResponse<ProductDto>> GetProductAsync(int id)
    {
        var res = await _repository.GetProductAsync(id);
        return res != null
            ? new ApiResponse<ProductDto>(HttpStatusCode.OK, "Product retrieved successfully", new ProductDto
            {
                Id = res.Id,
                Name = res.Name,
                Price = res.Price,
                Quantity = res.Quantity,
                CategoryId = res.CategoryId
            })
            : new ApiResponse<ProductDto>(HttpStatusCode.NotFound, "Product not found", null);
    }

    public async Task<ApiResponse<bool>> UpdateProductAsync(int id, ProductDto product)
    {
        var model = new Product
        {
            Id = id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId
        };
        var res = await _repository.UpdateProductAsync(model);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Product updated successfully", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Failed to update product", false);
    }
}
