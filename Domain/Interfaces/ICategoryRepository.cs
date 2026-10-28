using Domain.Models;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int id);
    List<Category> GetCategories();
    Category? GetCategoryById(int id);
    int GetAmountOfCategories();
}
