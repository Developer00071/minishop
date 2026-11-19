using System.Net;
using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _repository = categoryRepository;

    public async Task<ApiResponse<bool>> AddCategoryAsync(CategoryDto category)
    {
        var model = new Category { Name = category.Name };
        var res = await _repository.AddCategoryAsync(model);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Category added successfully!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Failed to add category", false);
    }

    public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
    {
        var res = await _repository.DeleteCategoryAsync(id);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Category deleted successfully!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Failed to delete category", false);
    }

    public async Task<ApiResponse<List<CategoryDto>>> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetAllCategoriesAsync();
        var result = categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        }).ToList();

        return new ApiResponse<List<CategoryDto>>(
            HttpStatusCode.OK,
            "List of categories",
            result
        );
    }

    public async Task<ApiResponse<CategoryDto>> GetCategoryAsync(int id)
    {
        var res = await _repository.GetCategoryAsync(id);
        return res != null
            ? new ApiResponse<CategoryDto>(HttpStatusCode.OK, "Category found", new CategoryDto
            {
                Id = res.Id,
                Name = res.Name
            })
            : new ApiResponse<CategoryDto>(HttpStatusCode.NotFound, "Category not found", null);
    }

    public async Task<ApiResponse<bool>> UpdateCategoryAsync(int id, CategoryDto category)
    {
        var model = new Category { Id = id, Name = category.Name };
        var res = await _repository.UpdateCategoryAsync(model);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Category updated successfully!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Failed to update category", false);
    }
}
