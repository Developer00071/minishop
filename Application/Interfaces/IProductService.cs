using Application.DTOs;
using Application.Responses;

namespace Application.Interfaces;

public interface IProductService
{
    Task<ApiResponse<List<ProductDto>>> GetAllProducts();
    Task<ApiResponse<ProductDto>> GetProduct(int id);
    Task<ApiResponse<bool>> AddProduct(ProductDto product);
    Task<ApiResponse<bool>> UpdateProduct(ProductDto product);
    Task<ApiResponse<bool>> DeleteProduct(int id);
}
