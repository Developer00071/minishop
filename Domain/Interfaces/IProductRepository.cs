using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    Task<int> AddProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> GetAmountOfProductsAsync(CancellationToken cancellationToken = default);
    Task<int> GetAmountOfProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<decimal?> GetMinPriceOfProductByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<decimal?> GetMaxPriceOfProductByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<decimal?> GetAveragePriceOfProductByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalPriceOfProductsAsync(CancellationToken cancellationToken = default);
}
