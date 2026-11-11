using Applicatiion.DTOs;
using Applicatiion.Requests;

namespace Applicatiion.Services;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<CategoryDto> CreateAsync(CategoryRequest request, CancellationToken cancellationToken);
    Task<CategoryDto?> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<int> GetCountAsync(CancellationToken cancellationToken);
}
