using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    public void AddCategory(Category category)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        conn.Execute("INSERT INTO categories (name) VALUES (@Name)", category);
    }

    public void UpdateCategory(Category category)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        conn.Execute("UPDATE categories SET name = @Name WHERE id = @Id", category);
    }

    public void DeleteCategory(int id)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        conn.Execute("DELETE FROM categories WHERE id = @id", new { id });
    }

    public List<Category> GetCategories()
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.Query<Category>("SELECT id, name FROM categories").ToList();
    }

    public Category? GetCategoryById(int id)
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.QuerySingleOrDefault<Category>(
            "SELECT id, name FROM categories WHERE id = @id",
            new { id });
    }

    public int GetAmountOfCategories()
    {
        using var conn = Database.Database.connection();
        conn.Open();
        return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM categories");
    }
}
