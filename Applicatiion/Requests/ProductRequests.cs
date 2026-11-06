using System.ComponentModel.DataAnnotations;

namespace Applicatiion.Requests;

/// <summary>Fields required to create a product.</summary>
public sealed record CreateProductRequest
{
    [Required, MaxLength(200)]
    public required string Name { get; init; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; init; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; init; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; init; }
}

/// <summary>Fields used to replace an existing product.</summary>
public sealed record UpdateProductRequest
{
    [Required, MaxLength(200)]
    public required string Name { get; init; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; init; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; init; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; init; }
}

/// <summary>Product details returned by the API.</summary>
public sealed record ProductResponse(int Id, string Name, decimal Price, int Quantity, int CategoryId);
