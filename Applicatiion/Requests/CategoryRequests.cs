using System.ComponentModel.DataAnnotations;

namespace Applicatiion.Requests;

/// <summary>Fields required to create or update a category.</summary>
public sealed record CategoryRequest
{
    [Required, MaxLength(200)]
    public required string Name { get; init; }
}

/// <summary>Category details returned by the API.</summary>
public sealed record CategoryResponse(int Id, string Name);
