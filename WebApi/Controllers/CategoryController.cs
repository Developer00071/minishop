using System.Net;
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
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<CategoryResponse>>>> GetAll(
        CancellationToken cancellationToken)
    {
        var categories = await categoryService.GetAllAsync(cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<CategoryResponse>>(
            HttpStatusCode.OK, "Categories retrieved successfully.", categories));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CategoryResponse>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CategoryResponse>>> GetById(
        int id, CancellationToken cancellationToken)
    {
        var category = await categoryService.GetByIdAsync(id, cancellationToken);
        return category is null
            ? NotFound(new ApiResponse<CategoryResponse>(HttpStatusCode.NotFound, "Category not found."))
            : Ok(new ApiResponse<CategoryResponse>(HttpStatusCode.OK, "Category retrieved successfully.", category));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CategoryResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<CategoryResponse>>> Create(
        CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await categoryService.CreateAsync(request, cancellationToken);
        var response = new ApiResponse<CategoryResponse>(
            HttpStatusCode.Created, "Category created successfully.", category);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CategoryResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<CategoryResponse>>> Update(
        int id, CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await categoryService.UpdateAsync(id, request, cancellationToken);
        return category is null
            ? NotFound(new ApiResponse<CategoryResponse>(HttpStatusCode.NotFound, "Category not found."))
            : Ok(new ApiResponse<CategoryResponse>(HttpStatusCode.OK, "Category updated successfully.", category));
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
