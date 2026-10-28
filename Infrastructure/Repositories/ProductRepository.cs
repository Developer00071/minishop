using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private const string ProductColumns = "id, name, price, quantity, category_id AS \"CategoryId\"";

    public void AddProduct(Product product)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        conn.Execute(
            "INSERT INTO products (name, price, quantity, category_id) VALUES (@Name, @Price, @Quantity, @CategoryId)",
            product);
    }

    public void UpdateProduct(Product product)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        conn.Execute(
            "UPDATE products SET name = @Name, price = @Price, quantity = @Quantity, category_id = @CategoryId WHERE id = @Id",
            product);
    }

    public void DeleteProduct(int id)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        conn.Execute("DELETE FROM products WHERE id = @id", new { id });
    }

    public List<Product> GetProducts()
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.Query<Product>($"SELECT {ProductColumns} FROM products").ToList();
    }

    public List<Product> GetProductsByCategory(int categoryId)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.Query<Product>(
            $"SELECT {ProductColumns} FROM products WHERE category_id = @categoryId",
            new { categoryId }).ToList();
    }

    public Product? GetProductById(int id)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.QuerySingleOrDefault<Product>(
            $"SELECT {ProductColumns} FROM products WHERE id = @id",
            new { id });
    }

    public int GetAmountOfProducts()
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM products");
    }

    public int GetAmountOfProductsByCategory(int categoryId)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM products WHERE category_id = @categoryId",
            new { categoryId });
    }

    public decimal? GetMinPriceOfProductByCategory(int categoryId)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.ExecuteScalar<decimal?>(
            "SELECT MIN(price) FROM products WHERE category_id = @categoryId",
            new { categoryId });
    }

    public decimal? GetMaxPriceOfProductByCategory(int categoryId)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.ExecuteScalar<decimal?>(
            "SELECT MAX(price) FROM products WHERE category_id = @categoryId",
            new { categoryId });
    }

    public decimal? GetAveragePriceOfProductByCategory(int categoryId)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.ExecuteScalar<decimal?>(
            "SELECT AVG(price) FROM products WHERE category_id = @categoryId",
            new { categoryId });
    }

    public decimal GetTotalPriceOfProducts()
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.ExecuteScalar<decimal>("SELECT COALESCE(SUM(price), 0) FROM products");
    }
}
