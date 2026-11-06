using Applicatiion.Requests;
using Domain.Interfaces;
using Domain.Models;

namespace Applicatiion.Services;

public sealed class ProductService(IProductRepository repository) : IProductService
{
    public async Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await repository.GetProductsAsync(cancellationToken);
        return products.Select(ToResponse).ToList();
    }

    public async Task<IReadOnlyList<ProductResponse>> GetByCategoryAsync(
        int categoryId, CancellationToken cancellationToken)
    {
        var products = await repository.GetProductsByCategoryAsync(categoryId, cancellationToken);
        return products.Select(ToResponse).ToList();
    }

    public async Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var product = await repository.GetProductByIdAsync(id, cancellationToken);
        return product is null ? null : ToResponse(product);
    }

    public async Task<ProductResponse> CreateAsync(
        CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId
        };
        product.Id = await repository.AddProductAsync(product, cancellationToken);
        return ToResponse(product);
    }

    public async Task<ProductResponse?> UpdateAsync(
        int id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = id,
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId
        };
        return await repository.UpdateProductAsync(product, cancellationToken)
            ? ToResponse(product)
            : null;
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken) =>
        repository.DeleteProductAsync(id, cancellationToken);

    public Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        repository.GetAmountOfProductsAsync(cancellationToken);

    public Task<int> GetCountByCategoryAsync(int categoryId, CancellationToken cancellationToken) =>
        repository.GetAmountOfProductsByCategoryAsync(categoryId, cancellationToken);

    public Task<decimal?> GetMinimumPriceByCategoryAsync(int categoryId, CancellationToken cancellationToken) =>
        repository.GetMinPriceOfProductByCategoryAsync(categoryId, cancellationToken);

    public Task<decimal?> GetMaximumPriceByCategoryAsync(int categoryId, CancellationToken cancellationToken) =>
        repository.GetMaxPriceOfProductByCategoryAsync(categoryId, cancellationToken);

    public Task<decimal?> GetAveragePriceByCategoryAsync(int categoryId, CancellationToken cancellationToken) =>
        repository.GetAveragePriceOfProductByCategoryAsync(categoryId, cancellationToken);

    public Task<decimal> GetTotalPriceAsync(CancellationToken cancellationToken) =>
        repository.GetTotalPriceOfProductsAsync(cancellationToken);

    private static ProductResponse ToResponse(Product product) =>
        new(product.Id, product.Name, product.Price, product.Quantity, product.CategoryId);
}
