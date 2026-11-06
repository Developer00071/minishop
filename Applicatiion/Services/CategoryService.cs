using Applicatiion.Requests;
using Domain.Interfaces;
using Domain.Models;

namespace Applicatiion.Services;

public sealed class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<IReadOnlyList<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await repository.GetCategoriesAsync(cancellationToken);
        return categories.Select(ToResponse).ToList();
    }

    public async Task<CategoryResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var category = await repository.GetCategoryByIdAsync(id, cancellationToken);
        return category is null ? null : ToResponse(category);
    }

    public async Task<CategoryResponse> CreateAsync(
        CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category { Name = request.Name };
        category.Id = await repository.AddCategoryAsync(category, cancellationToken);
        return ToResponse(category);
    }

    public async Task<CategoryResponse?> UpdateAsync(
        int id, CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category { Id = id, Name = request.Name };
        return await repository.UpdateCategoryAsync(category, cancellationToken)
            ? ToResponse(category)
            : null;
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken) =>
        repository.DeleteCategoryAsync(id, cancellationToken);

    public Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        repository.GetAmountOfCategoriesAsync(cancellationToken);

    private static CategoryResponse ToResponse(Category category) =>
        new(category.Id, category.Name);
}
