using System.Net;
using Applicatiion.Requests;
using Applicatiion.Responses;
using Applicatiion.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ProductResponse>>>> GetAll(
        CancellationToken cancellationToken)
    {
        var products = await productService.GetAllAsync(cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<ProductResponse>>(
            HttpStatusCode.OK, "Products retrieved successfully.", products));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<ProductResponse>>> GetById(
        int id, CancellationToken cancellationToken)
    {
        var product = await productService.GetByIdAsync(id, cancellationToken);
        return product is null
            ? NotFound(new ApiResponse<ProductResponse>(HttpStatusCode.NotFound, "Product not found."))
            : Ok(new ApiResponse<ProductResponse>(HttpStatusCode.OK, "Product retrieved successfully.", product));
    }

    [HttpGet("category/{categoryId:int}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ProductResponse>>>> GetByCategory(
        int categoryId, CancellationToken cancellationToken)
    {
        var products = await productService.GetByCategoryAsync(categoryId, cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<ProductResponse>>(
            HttpStatusCode.OK, "Products retrieved successfully.", products));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<ProductResponse>>> Create(
        CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await productService.CreateAsync(request, cancellationToken);
        var response = new ApiResponse<ProductResponse>(
            HttpStatusCode.Created, "Product created successfully.", product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<ProductResponse>>> Update(
        int id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await productService.UpdateAsync(id, request, cancellationToken);
        return product is null
            ? NotFound(new ApiResponse<ProductResponse>(HttpStatusCode.NotFound, "Product not found."))
            : Ok(new ApiResponse<ProductResponse>(HttpStatusCode.OK, "Product updated successfully.", product));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(
        int id, CancellationToken cancellationToken)
    {
        var deleted = await productService.DeleteAsync(id, cancellationToken);
        return deleted
            ? Ok(new ApiResponse<object>(HttpStatusCode.OK, "Product deleted successfully.", new { }))
            : NotFound(new ApiResponse<object>(HttpStatusCode.NotFound, "Product not found."));
    }

    [HttpGet("statistics/count")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<int>>> GetCount(CancellationToken cancellationToken)
    {
        var count = await productService.GetCountAsync(cancellationToken);
        return Ok(new ApiResponse<int>(HttpStatusCode.OK, "Product count retrieved successfully.", count));
    }

    [HttpGet("statistics/category/{categoryId:int}/count")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<int>>> GetCountByCategory(
        int categoryId, CancellationToken cancellationToken)
    {
        var count = await productService.GetCountByCategoryAsync(categoryId, cancellationToken);
        return Ok(new ApiResponse<int>(HttpStatusCode.OK, "Product count retrieved successfully.", count));
    }

    [HttpGet("statistics/category/{categoryId:int}/min-price")]
    [ProducesResponseType(typeof(ApiResponse<decimal?>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<decimal?>>> GetMinimumPriceByCategory(
        int categoryId, CancellationToken cancellationToken)
    {
        var price = await productService.GetMinimumPriceByCategoryAsync(categoryId, cancellationToken);
        return Ok(new ApiResponse<decimal?>(
            HttpStatusCode.OK, "Minimum product price retrieved successfully.", price));
    }

    [HttpGet("statistics/category/{categoryId:int}/max-price")]
    [ProducesResponseType(typeof(ApiResponse<decimal?>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<decimal?>>> GetMaximumPriceByCategory(
        int categoryId, CancellationToken cancellationToken)
    {
        var price = await productService.GetMaximumPriceByCategoryAsync(categoryId, cancellationToken);
        return Ok(new ApiResponse<decimal?>(
            HttpStatusCode.OK, "Maximum product price retrieved successfully.", price));
    }

    [HttpGet("statistics/category/{categoryId:int}/average-price")]
    [ProducesResponseType(typeof(ApiResponse<decimal?>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<decimal?>>> GetAveragePriceByCategory(
        int categoryId, CancellationToken cancellationToken)
    {
        var price = await productService.GetAveragePriceByCategoryAsync(categoryId, cancellationToken);
        return Ok(new ApiResponse<decimal?>(
            HttpStatusCode.OK, "Average product price retrieved successfully.", price));
    }

    [HttpGet("statistics/total-price")]
    [ProducesResponseType(typeof(ApiResponse<decimal>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<decimal>>> GetTotalPrice(CancellationToken cancellationToken)
    {
        var total = await productService.GetTotalPriceAsync(cancellationToken);
        return Ok(new ApiResponse<decimal>(HttpStatusCode.OK, "Total product price retrieved successfully.", total));
    }
}
