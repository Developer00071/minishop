using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        var res = await _context.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is null)
        {
            return false;
        }

        _context.Categories.Remove(category);
        var res = await _context.SaveChangesAsync();
        return res > 0;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
        if (existingCategory is null)
        {
            return false;
        }

        existingCategory.Name = category.Name;
        var res = await _context.SaveChangesAsync();
        return res > 0;
    }
}