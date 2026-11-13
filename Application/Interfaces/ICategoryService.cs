using Application.DTOs;
using Application.Responses;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<ApiResponse<List<CategoryDto>>> GetAllCategories();
    Task<ApiResponse<CategoryDto>> GetCategory(int id);
    Task<ApiResponse<bool>> AddCategory(CategoryDto category);
    Task<ApiResponse<bool>> UpdateCategory(CategoryDto category);
    Task<ApiResponse<bool>> DeleteCategory(int id);
}
