using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<List<Product>> GetProductsAsync();
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<Product?> GetProductByIdAsync(int id);
    Task<int> GetAmountOfProductsAsync();
    Task<int> GetAmountOfProductsByCategoryAsync(int categoryId);
    Task<decimal?> GetMinPriceOfProductByCategoryAsync(int categoryId);
    Task<decimal?> GetMaxPriceOfProductByCategoryAsync(int categoryId);
    Task<decimal?> GetAveragePriceOfProductByCategoryAsync(int categoryId);
    Task<decimal> GetTotalPriceOfProductsAsync();
}
