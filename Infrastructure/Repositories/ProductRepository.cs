
using Application.Data;
using Dapper;
using Domain.Models;
using Domain.interfaces;
namespace Infrastructure.Repositories;

public class ProductRepository(Database database) : IProductRepository
{
    private Database _data = database;

    public async Task<List<Product>> GetAllProducts()
    {
        using var con = _data.Connection();
        var query = "select id, name, price, quantity, category_id from products";
        var products = await con.QueryAsync<Product>(query);
        return products.ToList();
    }

    public async Task<Product?> GetProduct(int id)
    {
        using var con = _data.Connection();
        var query = "select id, name, price, quantity, category_id from products where id = @id";
        var product = await con.QueryFirstOrDefaultAsync<Product>(query , new { id });
        return product;
    }

    public async Task<bool> AddProduct(Product p)
    {
        using var con = _data.Connection();
        var query = @"
        insert into products(name, price, quantity, category_id)
        values (@Name, @Price, @Quantity, @CategoryId) returning id";
        int id = await con.ExecuteScalarAsync<int>(query, p);
        return id > 0;
    }

    public async Task<bool> UpdateProduct(Product p)
    {
        using var con = _data.Connection();
        var query = @"
        update products
        set name = @Name,
        price = @Price,
        quantity = @Quantity,
        category_id = @CategoryId
        where id = @Id";
        int rows = await con.ExecuteAsync(query, p);
        return rows > 0;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        using var con = _data.Connection();
        var query = "delete from products where id = @id";
        int rows = await con.ExecuteAsync(query, new { id });
        return rows > 0;
    }

    public async Task SearchProductByName(string name)
    {
        using var con = _data.Connection();
        var query = @"
        select id, name, price, quantity, category_id
        from products
        where name = @name";
        var products = await con.QueryAsync<Product>(query, new { name });
        foreach (var product in products)
        {
            Console.WriteLine(
                $"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, CategoryId: {product.CategoryId}"
            );
        }
    }

    public async Task<decimal> Filter(decimal maxPrice)
    {
        using var con = _data.Connection();
        var query = "select max(price) from products where price <= @maxPrice";
        var price = await con.ExecuteScalarAsync<decimal?>(query, new { maxPrice });
        return price ?? 0;
    }
}
