
using Application.Data;
using Dapper;
using Domain.interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class CategoryRepository(Database database) : ICategoryRepository
{
    private Database _data = database;

    public async Task<List<Category>> GetAllCategories()
    {
        using var con = _data.Connection();
        var query = "select id, name from categories";
        var categories = await con.QueryAsync<Category>(query);
        return categories.ToList();
    }

    public async Task<Category?> GetCategory(int id)
    {
        using var con = _data.Connection();
        var query = "select id, name from categories where id = @id";
        var category = await con.QueryFirstOrDefaultAsync<Category>(query , new { id });
        return category;
    }

    public async Task<bool> AddCategory(Category c)
    {
        using var con = _data.Connection();
        var query = "insert into categories(name) values (@name)";
        int id = await con.ExecuteAsync(query, new{name = c.Name});
        return id > 0;
    }

    public async Task<bool> UpdateCategory(Category c)
    {
        using var con = _data.Connection();
        var query = "update categories set name = @name where id = @id";
        int rows = await con.ExecuteAsync(query, new {id = c.Id , name = c.Name});
        return rows > 0;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        using var con = _data.Connection();
        var query = "delete from categories where id = @id";
        int rows = await con.ExecuteAsync(query, new { id });
        return rows > 0;
    }
}