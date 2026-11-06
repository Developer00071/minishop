using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    public async Task<int> AddCategoryAsync(Category category, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<int>(new CommandDefinition(
            "INSERT INTO categories (name) VALUES (@Name) RETURNING id",
            category,
            cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateCategoryAsync(Category category, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        var affectedRows = await conn.ExecuteAsync(new CommandDefinition(
            "UPDATE categories SET name = @Name WHERE id = @Id",
            category,
            cancellationToken: cancellationToken));
        return affectedRows > 0;
    }

    public async Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        var affectedRows = await conn.ExecuteAsync(new CommandDefinition(
            "DELETE FROM categories WHERE id = @id",
            new { id },
            cancellationToken: cancellationToken));
        return affectedRows > 0;
    }

    public async Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        var categories = await conn.QueryAsync<Category>(new CommandDefinition(
            "SELECT id, name FROM categories",
            cancellationToken: cancellationToken));
        return categories.ToList();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.QuerySingleOrDefaultAsync<Category>(new CommandDefinition(
            "SELECT id, name FROM categories WHERE id = @id",
            new { id },
            cancellationToken: cancellationToken));
    }

    public async Task<int> GetAmountOfCategoriesAsync(CancellationToken cancellationToken = default)
    {
        await using var conn = Database.Database.connection();
        await conn.OpenAsync(cancellationToken);
        return await conn.ExecuteScalarAsync<int>(new CommandDefinition(
            "SELECT COUNT(*) FROM categories",
            cancellationToken: cancellationToken));
    }
}
