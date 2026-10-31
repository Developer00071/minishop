using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private const string ProductColumns = "id, name, price, quantity, category_id AS \"CategoryId\"";

    public async Task AddProductAsync(Product product)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        await conn.ExecuteAsync(
            "INSERT INTO products (name, price, quantity, category_id) VALUES (@Name, @Price, @Quantity, @CategoryId)",
            product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        await conn.ExecuteAsync(
            "UPDATE products SET name = @Name, price = @Price, quantity = @Quantity, category_id = @CategoryId WHERE id = @Id",
            product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        await conn.ExecuteAsync("DELETE FROM products WHERE id = @id", new { id });
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        var products = await conn.QueryAsync<Product>($"SELECT {ProductColumns} FROM products");
        return products.ToList();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        var products = await conn.QueryAsync<Product>(
            $"SELECT {ProductColumns} FROM products WHERE category_id = @categoryId",
            new { categoryId });
        return products.ToList();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.QuerySingleOrDefaultAsync<Product>(
            $"SELECT {ProductColumns} FROM products WHERE id = @id",
            new { id });
    }

    public async Task<int> GetAmountOfProductsAsync()
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM products");
    }

    public async Task<int> GetAmountOfProductsByCategoryAsync(int categoryId)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM products WHERE category_id = @categoryId",
            new { categoryId });
    }

    public async Task<decimal?> GetMinPriceOfProductByCategoryAsync(int categoryId)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.ExecuteScalarAsync<decimal?>(
            "SELECT MIN(price) FROM products WHERE category_id = @categoryId",
            new { categoryId });
    }

    public async Task<decimal?> GetMaxPriceOfProductByCategoryAsync(int categoryId)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.ExecuteScalarAsync<decimal?>(
            "SELECT MAX(price) FROM products WHERE category_id = @categoryId",
            new { categoryId });
    }

    public async Task<decimal?> GetAveragePriceOfProductByCategoryAsync(int categoryId)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.ExecuteScalarAsync<decimal?>(
            "SELECT AVG(price) FROM products WHERE category_id = @categoryId",
            new { categoryId });
    }

    public async Task<decimal> GetTotalPriceOfProductsAsync()
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.ExecuteScalarAsync<decimal>("SELECT COALESCE(SUM(price), 0) FROM products");
    }
}
