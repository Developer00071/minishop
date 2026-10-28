using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
    List<Product> GetProducts();
    List<Product> GetProductsByCategory(int categoryId);
    Product? GetProductById(int id);
    int GetAmountOfProducts();
    int GetAmountOfProductsByCategory(int categoryId);
    decimal? GetMinPriceOfProductByCategory(int categoryId);
    decimal? GetMaxPriceOfProductByCategory(int categoryId);
    decimal? GetAveragePriceOfProductByCategory(int categoryId);
    decimal GetTotalPriceOfProducts();
}
