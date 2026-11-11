using System.Net;
using Applicatiion.DTOs;
using Applicatiion.Requests;
using Applicatiion.Responses;
using Applicatiion.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<CategoryDto>>>> GetAll(
        CancellationToken cancellationToken)
    {
        var categories = await categoryService.GetAllAsync(cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<CategoryDto>>(
            HttpStatusCode.OK, "Categories retrieved successfully.", categories));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> GetById(
        int id, CancellationToken cancellationToken)
    {
        var category = await categoryService.GetByIdAsync(id, cancellationToken);
        return category is null
            ? NotFound(new ApiResponse<CategoryDto>(HttpStatusCode.NotFound, "Category not found."))
            : Ok(new ApiResponse<CategoryDto>(HttpStatusCode.OK, "Category retrieved successfully.", category));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> Create(
        CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await categoryService.CreateAsync(request, cancellationToken);
        var response = new ApiResponse<CategoryDto>(
            HttpStatusCode.Created, "Category created successfully.", category);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> Update(
        int id, CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await categoryService.UpdateAsync(id, request, cancellationToken);
        return category is null
            ? NotFound(new ApiResponse<CategoryDto>(HttpStatusCode.NotFound, "Category not found."))
            : Ok(new ApiResponse<CategoryDto>(HttpStatusCode.OK, "Category updated successfully.", category));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(
        int id, CancellationToken cancellationToken)
    {
        var deleted = await categoryService.DeleteAsync(id, cancellationToken);
        return deleted
            ? Ok(new ApiResponse<object>(HttpStatusCode.OK, "Category deleted successfully.", new { }))
            : NotFound(new ApiResponse<object>(HttpStatusCode.NotFound, "Category not found."));
    }

    [HttpGet("statistics/count")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<int>>> GetCount(CancellationToken cancellationToken)
    {
        var count = await categoryService.GetCountAsync(cancellationToken);
        return Ok(new ApiResponse<int>(HttpStatusCode.OK, "Category count retrieved successfully.", count));
    }
}
