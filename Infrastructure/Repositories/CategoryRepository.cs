using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    public async Task AddCategoryAsync(Category category)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        await conn.ExecuteAsync("INSERT INTO categories (name) VALUES (@Name)", category);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        await conn.ExecuteAsync("UPDATE categories SET name = @Name WHERE id = @Id", category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        await conn.ExecuteAsync("DELETE FROM categories WHERE id = @id", new { id });
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        var categories = await conn.QueryAsync<Category>("SELECT id, name FROM categories");
        return categories.ToList();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.QuerySingleOrDefaultAsync<Category>(
            "SELECT id, name FROM categories WHERE id = @id",
            new { id });
    }

    public async Task<int> GetAmountOfCategoriesAsync()
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync();
        return await conn.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM categories");
    }
}
