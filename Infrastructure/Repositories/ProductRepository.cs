using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private const string ProductColumns = "id, name, price, quantity, category_id AS \"CategoryId\"";

    public async Task<int> AddProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<int>(new CommandDefinition(
            "INSERT INTO products (name, price, quantity, category_id) VALUES (@Name, @Price, @Quantity, @CategoryId) RETURNING id",
            product,
            cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        var affectedRows = await conn.ExecuteAsync(new CommandDefinition(
            "UPDATE products SET name = @Name, price = @Price, quantity = @Quantity, category_id = @CategoryId WHERE id = @Id",
            product,
            cancellationToken: cancellationToken));
        return affectedRows > 0;
    }

    public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        var affectedRows = await conn.ExecuteAsync(new CommandDefinition(
            "DELETE FROM products WHERE id = @id",
            new { id },
            cancellationToken: cancellationToken));
        return affectedRows > 0;
    }

    public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        var products = await conn.QueryAsync<Product>(new CommandDefinition(
            $"SELECT {ProductColumns} FROM products",
            cancellationToken: cancellationToken));
        return products.ToList();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        var products = await conn.QueryAsync<Product>(new CommandDefinition(
            $"SELECT {ProductColumns} FROM products WHERE category_id = @categoryId",
            new { categoryId },
            cancellationToken: cancellationToken));
        return products.ToList();
    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.QuerySingleOrDefaultAsync<Product>(new CommandDefinition(
            $"SELECT {ProductColumns} FROM products WHERE id = @id",
            new { id },
            cancellationToken: cancellationToken));
    }

    public async Task<int> GetAmountOfProductsAsync(CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<int>(new CommandDefinition(
            "SELECT COUNT(*) FROM products",
            cancellationToken: cancellationToken));
    }

    public async Task<int> GetAmountOfProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<int>(new CommandDefinition(
            "SELECT COUNT(*) FROM products WHERE category_id = @categoryId",
            new { categoryId },
            cancellationToken: cancellationToken));
    }

    public async Task<decimal?> GetMinPriceOfProductByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<decimal?>(new CommandDefinition(
            "SELECT MIN(price) FROM products WHERE category_id = @categoryId",
            new { categoryId },
            cancellationToken: cancellationToken));
    }

    public async Task<decimal?> GetMaxPriceOfProductByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<decimal?>(new CommandDefinition(
            "SELECT MAX(price) FROM products WHERE category_id = @categoryId",
            new { categoryId },
            cancellationToken: cancellationToken));
    }

    public async Task<decimal?> GetAveragePriceOfProductByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<decimal?>(new CommandDefinition(
            "SELECT AVG(price) FROM products WHERE category_id = @categoryId",
            new { categoryId },
            cancellationToken: cancellationToken));
    }

    public async Task<decimal> GetTotalPriceOfProductsAsync(CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<decimal>(new CommandDefinition(
            "SELECT COALESCE(SUM(price), 0) FROM products",
            cancellationToken: cancellationToken));
    }
}
