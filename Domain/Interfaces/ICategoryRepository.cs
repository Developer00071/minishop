using Domain.Models;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryAsync(int id);
    Task<bool> AddCategoryAsync(Category c);
    Task<bool> UpdateCategoryAsync(Category c);
    Task<bool> DeleteCategoryAsync(int id);
}
