using Domain.Models;

namespace Domain.interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategories();
    Task<Category?> GetCategory(int id);
    Task<bool> AddCategory(Category c);
    Task<bool> UpdateCategory(Category c);
    Task<bool> DeleteCategory(int id);
}
