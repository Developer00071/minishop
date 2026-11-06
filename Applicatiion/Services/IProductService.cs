using Applicatiion.Requests;

namespace Applicatiion.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductResponse>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProductResponse> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken);
    Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<int> GetCountAsync(CancellationToken cancellationToken);
    Task<int> GetCountByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<decimal?> GetMinimumPriceByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<decimal?> GetMaximumPriceByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<decimal?> GetAveragePriceByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<decimal> GetTotalPriceAsync(CancellationToken cancellationToken);
}
