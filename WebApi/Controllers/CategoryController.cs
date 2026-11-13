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
    public async Task<ApiResponse<List<CategoryDto>>> GetAllCategories()
    {
        return await _service.GetAllCategories();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<CategoryDto>> GetCategory(int id)
    {
        return await _service.GetCategory(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> AddCategory(CategoryDto category)
    {
        return await _service.AddCategory(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> DeleteCategory(int id)
    {
        return await _service.DeleteCategory(id);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> UpdateCategory(CategoryDto category)
    {
        return await _service.UpdateCategory(category);
    }
}
