using Domain.Models;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    Task<int> AddCategoryAsync(Category category, CancellationToken cancellationToken = default);
    Task<bool> UpdateCategoryAsync(Category category, CancellationToken cancellationToken = default);
    Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> GetAmountOfCategoriesAsync(CancellationToken cancellationToken = default);
}
