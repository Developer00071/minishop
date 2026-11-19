using Application.DTOs;
using Application.Responses;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<ApiResponse<List<CategoryDto>>> GetAllCategoriesAsync();
    Task<ApiResponse<CategoryDto>> GetCategoryAsync(int id);
    Task<ApiResponse<bool>> AddCategoryAsync(CategoryDto category);
    Task<ApiResponse<bool>> UpdateCategoryAsync(int id, CategoryDto category);
    Task<ApiResponse<bool>> DeleteCategoryAsync(int id);
}
