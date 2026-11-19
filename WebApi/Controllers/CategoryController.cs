using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _service = categoryService;

    [HttpGet]
    public async Task<ApiResponse<List<CategoryDto>>> GetAllCategoriesAsync()
    {
        return await _service.GetAllCategoriesAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<CategoryDto>> GetCategoryAsync(int id)
    {
        return await _service.GetCategoryAsync(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> AddCategoryAsync(CategoryDto category)
    {
        return await _service.AddCategoryAsync(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
    {
        return await _service.DeleteCategoryAsync(id);
    }

    [HttpPut("{id:int}")]
    public async Task<ApiResponse<bool>> UpdateCategoryAsync(int id, CategoryDto category)
    {
        return await _service.UpdateCategoryAsync(id, category);
    }
}
