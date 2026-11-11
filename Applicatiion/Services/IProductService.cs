using Applicatiion.DTOs;
using Applicatiion.Requests;

namespace Applicatiion.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken);
    Task<ProductDto?> UpdateAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<int> GetCountAsync(CancellationToken cancellationToken);
    Task<int> GetCountByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<decimal?> GetMinimumPriceByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<decimal?> GetMaximumPriceByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<decimal?> GetAveragePriceByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<decimal> GetTotalPriceAsync(CancellationToken cancellationToken);
}
