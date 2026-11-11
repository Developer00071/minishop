using Applicatiion.DTOs;
using Applicatiion.Requests;
using Domain.Interfaces;
using Domain.Models;

namespace Applicatiion.Services;

public sealed class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await repository.GetCategoriesAsync(cancellationToken);
        return categories.Select(ToDto).ToList();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var category = await repository.GetCategoryByIdAsync(id, cancellationToken);
        return category is null ? null : ToDto(category);
    }

    public async Task<CategoryDto> CreateAsync(
        CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category { Name = request.Name };
        category.Id = await repository.AddCategoryAsync(category, cancellationToken);
        return ToDto(category);
    }

    public async Task<CategoryDto?> UpdateAsync(
        int id, CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category { Id = id, Name = request.Name };
        return await repository.UpdateCategoryAsync(category, cancellationToken)
            ? ToDto(category)
            : null;
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken) =>
        repository.DeleteCategoryAsync(id, cancellationToken);

    public Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        repository.GetAmountOfCategoriesAsync(cancellationToken);

    private static CategoryDto ToDto(Category category) =>
        new() { Id = category.Id, Name = category.Name };
}
