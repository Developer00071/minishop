using Domain.Models;

namespace Domain.interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllProducts();
    Task<Product?> GetProduct(int id);
    Task<bool> AddProduct(Product p);
    Task<bool> UpdateProduct(Product p);
    Task<bool> DeleteProduct(int id);
}
