using System.Net;
using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Domain.interfaces;
using Domain.Models;

namespace Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private IProductRepository _repository = productRepository;

    public async Task<ApiResponse<bool>> AddProduct(ProductDto product)
    {
        var model = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId
        };
        var res = await _repository.AddProduct(model);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Added!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not Added!", false);
    }

    public async Task<ApiResponse<bool>> DeleteProduct(int id)
    {
        var res = await _repository.DeleteProduct(id);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Deleted!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not Deleted!", false);
    }

    public async Task<ApiResponse<List<ProductDto>>> GetAllProducts()
    {
        var res = await _repository.GetAllProducts();
        var products = res.Select(p => new ProductDto
        {
            Name = p.Name,
            Price = p.Price,
            Quantity = p.Quantity,
            CategoryId = p.CategoryId
        }).ToList();
        return new ApiResponse<List<ProductDto>>(HttpStatusCode.OK, "Products retrieved successfully", products);
    }

    public async Task<ApiResponse<ProductDto>> GetProduct(int id)
    {
        var res = await _repository.GetProduct(id);
        return res != null
            ? new ApiResponse<ProductDto>(HttpStatusCode.OK, "Product retrieved successfully", new ProductDto
            {
                Name = res.Name,
                Price = res.Price,
                Quantity = res.Quantity,
                CategoryId = res.CategoryId
            })
            : new ApiResponse<ProductDto>(HttpStatusCode.NotFound, "Product not found", null);
    }

    public async Task<ApiResponse<bool>> UpdateProduct(ProductDto product)
    {
        var model = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId
        };
        var res = await _repository.UpdateProduct(model);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Product updated successfully", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Failed to update product", false);
    }
}
