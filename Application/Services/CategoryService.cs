using System.Net;
using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Domain.interfaces;
using Domain.Models;

namespace Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private ICategoryRepository _repository = categoryRepository;

    public async Task<ApiResponse<bool>> AddCategory(CategoryDto category)
    {
        var model = new Category { Name = category.Name };
        var res = await _repository.AddCategory(model);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Category added successfully!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Failed to add category", false);
    }

    public async Task<ApiResponse<bool>> DeleteCategory(int id)
    {
        var res = await _repository.DeleteCategory(id);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Category deleted successfully!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Failed to delete category", false);
    }

    public async Task<ApiResponse<List<CategoryDto>>> GetAllCategories()
    {
        var res = await _repository.GetAllCategories();

        var categories = new List<CategoryDto>();

        foreach (var item in res)
        {
            var category = new CategoryDto
            {
                Name = item.Name
            };

            categories.Add(category);
        }

        return new ApiResponse<List<CategoryDto>>(
            HttpStatusCode.OK,
            "List of categories",
            categories.ToList()
        );
    }

    public async Task<ApiResponse<CategoryDto>> GetCategory(int id)
    {
        var res = await _repository.GetCategory(id);
        return res != null
            ? new ApiResponse<CategoryDto>(HttpStatusCode.OK, "Category found", new CategoryDto {Name = res.Name})
            : new ApiResponse<CategoryDto>(HttpStatusCode.NotFound, "Category not found", null);
    }

    public async Task<ApiResponse<bool>> UpdateCategory(CategoryDto category)
    {
        var model = new Category {Name = category.Name };
        var res = await _repository.UpdateCategory(model);
        return res
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Category updated successfully!", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Failed to update category", false);
    }
}
