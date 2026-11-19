using Application.DTOs;
using Application.Responses;

namespace Application.Interfaces;

public interface IProductService
{
    Task<ApiResponse<List<ProductDto>>> GetAllProductsAsync();
    Task<ApiResponse<ProductDto>> GetProductAsync(int id);
    Task<ApiResponse<bool>> AddProductAsync(ProductDto product);
    Task<ApiResponse<bool>> UpdateProductAsync(int id, ProductDto product);
    Task<ApiResponse<bool>> DeleteProductAsync(int id);
}
