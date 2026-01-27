using MediatR;
using Api.BootCamp.Api.Response;

namespace Api.BootCamp.Aplication.Command.CreateProduct;

public record CreateProductoCommand(
    string Codigo,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    int CategoriaId,
    int CantidadStock
) : IRequest<ProductoResponse>
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Codigo))
            throw new ArgumentException("El código es obligatorio");

        if (string.IsNullOrWhiteSpace(Nombre))
            throw new ArgumentException("El nombre es obligatorio");

        if (Precio <= 0)
            throw new ArgumentException("El precio debe ser mayor a cero");

        if (CategoriaId <= 0)
            throw new ArgumentException("La categoría es obligatoria");

        if (CantidadStock < 0)
            throw new ArgumentException("El stock no puede ser negativo");
    }
}
